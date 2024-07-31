class ChatHubService {
    constructor(muteCamMicService, messageCountService) {
        this.hubUrl = `${ObjClient.HostHub.api}hubs/`;
        this.hubConnection = null;

        this.oneOnlineUserSource = new Subject();
        this.oneOnlineUser$ = this.oneOnlineUserSource.asObservable();

        this.oneOfflineUserSource = new Subject();
        this.oneOfflineUser$ = this.oneOfflineUserSource.asObservable();

        this.messagesThreadSource = new BehaviorSubject([]);
        this.messagesThread$ = this.messagesThreadSource.asObservable();

        this.muteCamMicService = muteCamMicService;
        this.messageCountService = messageCountService;

        this.blockChatSource = new Subject();
        this.blockChat$ = this.blockChatSource.asObservable();

        this.kickSource = new Subject();
        this.kick$ = this.kickSource.asObservable();
    }

    createHubConnection(user, roomId) {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl + 'chathub?roomId=' + roomId, {
                accessTokenFactory: () => user.token
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start().catch(err => console.log(err));

        this.hubConnection.on('NewMessage', message => {
            if (this.messageCountService.activeTabChat) {
                this.messageCountService.MessageCount = 0;
            } else {
                this.messageCountService.MessageCount += 1
            }

            this.messagesThreadSource.next(message);
            /*this.messagesThread$.pipe(take(1)).subscribe(messages => {
                this.messagesThreadSource.next([...messages, message])
            })*/
        })

        this.hubConnection.on('UserOnlineInGroup', (user) => {
            //this.onlineUsersSource.next(users);
            this.oneOnlineUserSource.next(user);
            this.toastr.success(user.displayName + ' has join room!')
        })

        this.hubConnection.on('UserOfflineInGroup', (user) => {
            // this.onlineUsers$.pipe(take(1)).subscribe(users => {
            //   this.onlineUsersSource.next([...users.filter(x => x.userName !== user.userName)])
            // })
            this.oneOfflineUserSource.next(user);
            this.toastr.warning(user.displayName + ' has left room!')
        })

        this.hubConnection.on('OnMuteMicro', ({ userId, mute }) => {
            this.muteCamMicService.Microphone = { userId, mute }
        })

        this.hubConnection.on('OnMuteCamera', ({ userId, mute }) => {
            this.muteCamMicService.Camera = { userId, mute }
        })

        this.hubConnection.on('OnShareScreen', (isShareScreen) => {
            this.muteCamMicService.ShareScreen = isShareScreen
        })

        this.hubConnection.on('OnShareScreenLastUser', ({ userIdTo, isShare }) => {
            this.muteCamMicService.LastShareScreen = { userIdTo: userIdTo, isShare }
        })

        this.hubConnection.on('OnUserIsSharing', currentUsername => {
            this.muteCamMicService.UserIsSharing = currentUsername
        })

        this.hubConnection.on('OnBlockChat', state => {
            this.blockChatSource.next(state);
        })

        this.hubConnection.on('OnMuteAllMicro', ({ userId, mute }) => {
            this.muteCamMicService.userIsMuteAllMicroSource.next({ userId, mute });
        })

        this.hubConnection.on('OnIsKicked', ({ userId, roomId }) => {
            this.kickSource.next({ userId, roomId });
        })
    }

    stopHubConnection() {
        if (this.hubConnection) {
            this.hubConnection.stop()
                .then(() => console.log('Hub connection stopped.'))
                .catch(error => console.log(error));
        }
    }

    async blockChat(state) {
        return this.hubConnection.invoke('BlockChat', state)
            .catch(error => console.log(error));
    }

    async sendMessage(content) {
        return this.hubConnection.invoke('SendMessage', { content })
            .catch(error => console.log(error));
    }

    async muteMicroPhone(mute) {
        return this.hubConnection.invoke('MuteMicro', mute)
            .catch(error => console.log(error));
    }

    async muteCamera(mute) {
        return this.hubConnection.invoke('MuteCamera', mute)
            .catch(error => console.log(error));
    }

    async shareScreen(roomId, isShareScreen) {
        return this.hubConnection.invoke('ShareScreen', roomId, isShareScreen)
            .catch(error => console.log(error));
    }

    async shareScreenToUser(roomId, username, isShareScreen) {
        return this.hubConnection.invoke('ShareScreenToUser', roomId, username, isShareScreen)
            .catch(error => console.log(error));
    }

    async muteAllMicro(userId, mute) {
        return this.hubConnection.invoke('MuteAllMicro', userId, mute)
            .catch(error => console.log(error));
    }

    async kickMember(roomId, userId) {
        return this.hubConnection.invoke('KickMember', roomId, userId)
            .catch(error => console.log(error));
    }
    // Implement other methods like muteMicroPhone, muteCamera, shareScreen, etc.
}
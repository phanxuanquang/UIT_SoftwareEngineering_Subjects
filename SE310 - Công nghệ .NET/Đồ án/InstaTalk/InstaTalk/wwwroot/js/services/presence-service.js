class PresenceService {
    constructor(utility) {
        this.hubUrl = `${ObjClient.HostHub.api}hubs/`;
        this.hubConnection = null;
        this.onlineUsersSource = new BehaviorSubject([]);
        this.onlineUsers$ = this.onlineUsersSource.asObservable();
        this.utility = utility;
    }

    createHubConnection(user) {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl + 'presence', {
                accessTokenFactory: () => user.token
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .then(() => {
                this.subscribeToHubEvents();
            })
            .catch(error => console.log(error));
    }

    subscribeToHubEvents() {
        this.hubConnection.on('UserIsOnline', (username) => {
            this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
                this.onlineUsersSource.next([...usernames, username]);
            });
            console.log(username + ' has connected');
        });

        this.hubConnection.on('UserIsOffline', (username) => {
            this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
                this.onlineUsersSource.next([...usernames.filter(x => x !== username)]);
            });
            console.log(username + ' has disconnected');
        });

        this.hubConnection.on('GetOnlineUsers', (usernames) => {
            this.onlineUsersSource.next(usernames);
        });

        this.hubConnection.on('CountMemberInGroup', ({ roomId, countMember }) => {
            this.utility.RoomCount = { roomId, countMember };
        });

        this.hubConnection.on('OnLockedUser', (val) => {
            this.utility.KickedOutUser = val;
        });
    }

    stopHubConnection() {
        if (this.hubConnection) {
            this.hubConnection.stop()
                .then(() => console.log('Hub connection stopped.'))
                .catch(error => console.log(error));
        }
    }
}

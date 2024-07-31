class StrangerHubService {
    constructor() {
        this.hubUrl = `${ObjClient.HostHub.api}hubs/`;
        this.hubConnection = null;

        this.joinStrangerSource = new Subject();
        this.joinStranger$ = this.joinStrangerSource.asObservable();
    }
    createHubConnection(user, roomId) {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl + 'stranger?roomId=' + roomId, {
                accessTokenFactory: () => user.token
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start().catch(err => console.log(err));

        this.hubConnection.on('JoinStrangerRoom', ({ roomId }) => {
            this.joinStrangerSource.next(roomId);
        })
    }

    stopHubConnection() {
        if (this.hubConnection) {
            this.hubConnection.stop()
                .then(() => console.log('Hub connection stopped.'))
                .catch(error => console.log(error));
        }
    }
}
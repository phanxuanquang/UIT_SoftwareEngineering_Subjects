const strangerService = new StrangerHubService();

strangerService.createHubConnection(ObjClient.User, ObjClient.Room.roomId);

strangerService.joinStranger$.subscribe(roomId => {
    if (roomId) {
        window.location.href = `/Stranger/Matching?RoomId=${roomId}`
    }
});

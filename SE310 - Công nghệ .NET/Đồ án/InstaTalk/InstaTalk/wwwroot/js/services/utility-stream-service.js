class UtilityStreamService {
    constructor() {
        this.roomSource = new ReplaySubject(1);
        this.room$ = this.roomSource.asObservable();

        this.roomEditSource = new ReplaySubject(1);
        this.roomEdit$ = this.roomEditSource.asObservable();

        this.roomCountSource = new Subject();
        this.roomCount$ = this.roomCountSource.asObservable();

        this.kickedOutUserSource = new Subject();
        this.kickedOutUser$ = this.kickedOutUserSource.asObservable();
    }

    set Room(value) {
        this.roomSource.next(value);
    }

    set RoomEdit(value) {
        this.roomEditSource.next(value);
    }

    set RoomCount(value) {
        this.roomCountSource.next(value);
    }

    set KickedOutUser(value) {
        this.kickedOutUserSource.next(value);
    }
}

// Usage:

const utility = new UtilityStreamService();


// Set values in the service as needed
utility.Room = { /* your room object */ };
utility.RoomEdit = { /* your room edit object */ };
utility.RoomCount = { /* your room count object */ };
utility.KickedOutUser = true; // or false

// You can also subscribe to the observables in the service:
utility.room$.subscribe((room) => {
    console.log('Room:', room);
});

utility.roomEdit$.subscribe((roomEdit) => {
    console.log('Room Edit:', roomEdit);
});

utility.roomCount$.subscribe((roomCount) => {
    console.log('Room Count:', roomCount);
});

utility.kickedOutUser$.subscribe((kickedOut) => {
    console.log('Kicked Out:', kickedOut);
});

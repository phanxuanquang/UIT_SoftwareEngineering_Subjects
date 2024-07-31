class MuteCamMicService {
    constructor() {
        this.muteMicro = null;
        this.muteMicroSource = new Subject();
        this.muteMicro$ = this.muteMicroSource.asObservable();

        this.muteCamera = null;
        this.muteCameraSource = new Subject();
        this.muteCamera$ = this.muteCameraSource.asObservable();

        this.shareScreenSource = new Subject();
        this.shareScreen$ = this.shareScreenSource.asObservable();

        this.lastShareScreenSource = new Subject();
        this.lastShareScreen$ = this.lastShareScreenSource.asObservable();

        this.shareScreenToLastUserSource = new Subject();
        this.shareScreenToLastUser$ = this.shareScreenToLastUserSource.asObservable();

        this.userIsSharingSource = new Subject();
        this.userIsSharing$ = this.userIsSharingSource.asObservable();

        this.userIsMuteAllMicroSource = new Subject();
        this.userIsMuteAllMicro$ = this.userIsMuteAllMicroSource.asObservable();
    }

    set Microphone(value) {
        this.muteMicro = value;
        this.muteMicroSource.next(value);
    }

    get Microphone() {
        return this.muteMicro;
    }

    set Camera(value) {
        this.muteCamera = value;
        this.muteCameraSource.next(value);
    }

    get Camera() {
        return this.muteCamera;
    }

    set ShareScreen(value) {
        this.shareScreenSource.next(value);
    }

    set LastShareScreen(value) {
        this.lastShareScreenSource.next(value);
    }

    set ShareScreenToLastUser(value) {
        this.shareScreenToLastUserSource.next(value);
    }

    set UserIsSharing(value) {
        this.userIsSharingSource.next(value);
    }
}

/*const muteCamMicService = new MuteCamMicService();

// Set values and subscribe to observables as needed
muteCamMicService.Microphone = { *//* your muteMicro value *//* };
muteCamMicService.Camera = { *//* your muteCamera value *//* };
muteCamMicService.ShareScreen = true; // or false
muteCamMicService.LastShareScreen = { *//* your lastShareScreen value *//* };
muteCamMicService.ShareScreenToLastUser = true; // or false
muteCamMicService.UserIsSharing = 'some_username';

// Subscribe to observables
muteCamMicService.muteMicro$.subscribe((value) => {
    console.log('Mute Microphone:', value);
});
muteCamMicService.muteCamera$.subscribe((value) => {
    console.log('Mute Camera:', value);
});
muteCamMicService.shareScreen$.subscribe((value) => {
    console.log('Share Screen:', value);
});
muteCamMicService.lastShareScreen$.subscribe((value) => {
    console.log('Last Share Screen:', value);
});
muteCamMicService.shareScreenToLastUser$.subscribe((value) => {
    console.log('Share Screen To Last User:', value);
});
muteCamMicService.userIsSharing$.subscribe((value) => {
    console.log('User Is Sharing:', value);
});*/
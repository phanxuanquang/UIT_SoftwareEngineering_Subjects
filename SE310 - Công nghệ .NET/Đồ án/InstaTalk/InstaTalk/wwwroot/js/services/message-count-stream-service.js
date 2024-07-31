class MessageCountStreamService {
    constructor() {
        this.activeTabChat = false;
        this.messageCount = 0;
        this.messageCountSource = new ReplaySubject(1);
        this.messageCount$ = this.messageCountSource.asObservable();
        this.activeTabChatSource = new ReplaySubject(1);
        this.activeTabChat$ = this.activeTabChatSource.asObservable();
    }

    setMessageCount(value) {
        this.messageCount = value;
        this.messageCountSource.next(value);
    }

    getMessageCount() {
        return this.messageCount;
    }

    setActiveTabChat(value) {
        this.activeTabChat = value;
        this.activeTabChatSource.next(value);
    }
}

/*// Usage:
const messageCountService = new MessageCountStreamService();

// Set values and subscribe to observables as needed
messageCountService.setMessageCount(5); // Set message count to 5
messageCountService.setActiveTabChat(true); // Set active tab chat to true

// Retrieve values from the service
const messageCount = messageCountService.getMessageCount();
const activeTabChat = messageCountService.activeTabChat;

// Subscribe to observables
messageCountService.messageCount$.subscribe((value) => {
    console.log('Message Count:', value);
});

messageCountService.activeTabChat$.subscribe((value) => {
    console.log('Active Tab Chat:', value);
});*/
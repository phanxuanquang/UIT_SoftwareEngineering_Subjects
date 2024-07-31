const config = {
    'iceServers': [
        {
            urls: "stun:stun.relay.metered.ca:80",
        },
        {
            urls: "turn:a.relay.metered.ca:80",
            username: "4af24cfab7a9e683a59be531",
            credential: "N7WeALiaXC9Ti5i0",
        },
        {
            urls: "turn:a.relay.metered.ca:80?transport=tcp",
            username: "4af24cfab7a9e683a59be531",
            credential: "N7WeALiaXC9Ti5i0",
        },
        {
            urls: "turn:a.relay.metered.ca:80?transport=udp",
            username: "4af24cfab7a9e683a59be531",
            credential: "N7WeALiaXC9Ti5i0",
        },
        {
            urls: "turn:a.relay.metered.ca:443",
            username: "4af24cfab7a9e683a59be531",
            credential: "N7WeALiaXC9Ti5i0",
        },
        {
            urls: "turn:a.relay.metered.ca:443?transport=tcp",
            username: "4af24cfab7a9e683a59be531",
            credential: "N7WeALiaXC9Ti5i0",
        },
        {
            urls: "turn:a.relay.metered.ca:443?transport=udp",
            username: "4af24cfab7a9e683a59be531",
            credential: "N7WeALiaXC9Ti5i0",
        },
    ]
};
document.addEventListener("DOMContentLoaded", () => {
    // <snippet_Connection>
    var sessionId = document.getElementsByName("Input.SessionId")[0].value;
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/srlogin?sessionId=" + sessionId+"")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    // </snippet_Connection>

    // <snippet_ReceiveMessage>
    connection.on("Connect", (user, message) => {
        document.getElementById("qrlogin").submit();
    });
    // </snippet_ReceiveMessage>

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });
    //connection.qs = { 'sessionId': document.getElementsByName("Input.SessionId")[0].value };
    // Start the connection.
    start();
});
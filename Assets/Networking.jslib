mergeInto(LibraryManager.library, {

socket: null,

messages: [],

outgoing: [],

ConsoleLog: function (s) {
    console.log(Pointer_stringify(s));
},

Hello: function () {
    window.alert("hiiiiiiiiiiiiii");
},

ConnectToServer: function () {
    this.socket = new WebSocket('ws://dtapgames.co.uk:8081/boats');
    self = this;
    this.messages = [];
    this.outgoing = [];
    this.socket.onmessage = function (event) {
        self.messages.push(event.data);
    };
    this.socket.onopen = function (event) {
        while (self.outgoing.length > 0) {
            self.socket.send(self.outgoing.shift());
        }
    };
    this.socket.onclose = function (event) {
        self.messages.push("{\"tag\":\"Disconnected\"}");
    };
},

GetData: function () {
    var ret;
    if (this.messages.length > 0) {
        ret = this.messages.shift();
    } else {
        ret = "{\"tag\": \"nothing\"}";
    }
    
    var bufferSize = lengthBytesUTF8(ret) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(ret, buffer, bufferSize);
    return buffer;
},

SendData: function (data) {
    var s = Pointer_stringify(data);
    console.log(s);
    if (this.socket && this.socket.readyState == WebSocket.OPEN) {
        this.socket.send(s);
    } else {
        this.outgoing.push(s);
    }
}

})
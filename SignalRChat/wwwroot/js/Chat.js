document.addEventListener("DOMContentLoaded", function () {

    var userName = prompt("please enter your name");

    var messageInput = document.getElementById("messageInp");

    var groupNameInp = document.getElementById("groupNameInp");

    var messageToGroupInp = document.getElementById("messageToGroupInp");

    messageInput.focus();

    var proxyConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();


    proxyConnection.start().then(function () {

        document.getElementById("sendMessageBtn").addEventListener("click", function (e) {
            e.preventDefault();
            proxyConnection.invoke("Send", userName, messageInput.value)

        });


        document.getElementById("joinGroupBtn").addEventListener("click", function (e) {
            e.preventDefault();
            proxyConnection.invoke("JoinGroup", groupNameInp.value, userName)

        });


        document.getElementById("sendMessageToGroupBtn").addEventListener("click", function (e) {
            e.preventDefault();
            proxyConnection.invoke("SendToGroup", groupNameInp.value, userName, messageToGroupInp.value)

        });


    });

    proxyConnection.on("ReciveMessage", function (userName, message) {

        var listElement = document.createElement('li')
        listElement.innerHTML = `<strong>${userName} </strong> :${message}`
        document.getElementById("conversation").appendChild(listElement);

    });







    proxyConnection.on("NewMemberJoin", function (groupName, userName) {

        var listElement = document.createElement('li')
        listElement.innerHTML = `<strong> ${userName} : </strong> has joined in  ${groupName}`
        document.getElementById("groupConversationUL").appendChild(listElement);

    });


    proxyConnection.on("ReciveMessageFromGroup", function (sender, message) {

        var listElement = document.createElement('li')
        listElement.innerHTML = `<strong>${sender}</strong> :${message}`
        document.getElementById("groupConversationUL").appendChild(listElement);

    });


});
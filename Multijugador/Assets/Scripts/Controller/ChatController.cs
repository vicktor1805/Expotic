using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using ExitGames.Client.Photon;

public class ChatController : MonoBehaviour,IChatClientListener {

    private string AppChatID;
    private ChatClient chatClient;
    private ChatChannel chatChannel;
    private Text textToSend;
    private Text chatMessage;
    private InputField inputToSend;
    private List<string> listMessages;

	void Start () {

        inputToSend = GameObject.Find("InputFieldToSendMessage").GetComponent<InputField>();
        listMessages = new List<string>();
        chatMessage = GameObject.Find("ChatMessage").GetComponent<Text>();
        textToSend = GameObject.Find("TextToSend").GetComponent<Text>();
        AppChatID = "a00e75c3-326c-472a-8f52-4c90184041aa";
        chatClient = new ChatClient(this);
        chatClient.Connect(AppChatID, "1.0", new ExitGames.Client.Photon.Chat.AuthenticationValues(PhotonNetwork.playerName));        
	}
	
	// Update is called once per frame
	void Update () {

        if (this.chatClient != null)
        {
            chatClient.Service();
        }
	}


    public void ChatSendMessage()
    {
        if (!string.IsNullOrEmpty(textToSend.text))
        {
            chatClient.PublishMessage("ChatChannel_" + PhotonNetwork.room.name, textToSend.text);
            inputToSend.text = string.Empty;
        }
      
    }
    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { "ChatChannel_" + PhotonNetwork.room.name });
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnDisconnected()
    {
    }

    public void OnChatStateChange(ChatState state)
    {
        // use OnConnected() and OnDisconnected()
        // this method might become more useful in the future, when more complex states are being used.
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to a new channel!");
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        int msgCount = messages.Length;
        for (int i = 0; i < msgCount; i++)
        { //go through each received msg 
            string sender = senders[i];
            string msg = messages[i].ToString();
            Debug.Log(sender + ": " + msg);
            string fullMessage =  sender + ": " + msg;
            listMessages.Add(fullMessage);
        }

        foreach (string msg in listMessages)
        {
            chatMessage.text += msg + "\n";
        }

        listMessages.Clear();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        // as the ChatClient is buffering the messages for you, this GUI doesn't need to do anything here
        // you also get messages that you sent yourself. in that case, the channelName is determinded by the target of your msg
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        // this is how you get status updates of friends.
        // this demo simply adds status updates to the currently shown chat.
        // you could buffer them or use them any other way, too.
    }
}

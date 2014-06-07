using System.Collections;
using Sfs2X;
using Sfs2X.Requests;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests.MMO;

public static class SmartfoxNetExtension
{
    #region Params
    public static object GetObject(this IDictionary dictionary, string key)
    {
        return dictionary.GetValue<object>(key);
    }

    public static T GetValue<T>(this IDictionary dictionary, string key)
    {
        if (dictionary == null)
            return default(T);

        if (!dictionary.Contains(key))
            return default(T);

        object obj = dictionary[key];

        if (obj is T)
            return (T)obj;

        return default(T);
    }

    public static Room GetRoom(this IDictionary dictionary) { return dictionary.GetValue<Room>("room"); }
    public static User GetUser(this IDictionary dictionary) { return dictionary.GetValue<User>("user"); }
    public static User GetSender(this IDictionary dictionary) { return dictionary.GetValue<User>("sender"); }
    public static string GetMessage(this IDictionary dictionary) { return dictionary.GetValue<string>("message"); }
    public static string GetCommande(this IDictionary dictionary) { return dictionary.GetValue<string>("cmd"); }
    public static ISFSObject GetParams(this IDictionary dictionary) { return dictionary.GetValue<ISFSObject>("params"); } 
    #endregion

    #region Requests
    #region Login
    public static void SendLoginRequest(this SmartFox smartfox, string username, string password, string zone)
    {
        if (!string.IsNullOrEmpty(zone))
            smartfox.Send(new LoginRequest(username, password, zone));
    }
    public static void SendLogoutRequest(this SmartFox smartfox)
    {
        smartfox.Send(new LogoutRequest());
    } 
    #endregion

    #region Room
    public static void SendJoinRoomRequest(this SmartFox smartfox, object id, string pass = null, int roomIdToLeave = default(int), bool asSpect = false)
    {
        smartfox.Send(new JoinRoomRequest(id, pass, roomIdToLeave, asSpect));
    }
    public static void SendLeaveRoomRequest(this SmartFox smartfox)
    {
        smartfox.Send(new LeaveRoomRequest());
    }

    public static void SendCreateRoomRequest(this SmartFox smartfox, string roomName, bool autojoin = true, Room roomToLeave = null)
    {
        MMORoomSettings settings = new MMORoomSettings(roomName);
        settings.DefaultAOI = new Vec3D(25f, 1f, 25f);
        settings.MapLimits = new MapLimits(new Vec3D(-100f, 1f, -100f), new Vec3D(100f, 1f, 100f));
        settings.MaxUsers = 100;
        settings.Extension = new RoomExtension("pyTest", "MMORoomDemo.py");

        smartfox.SendCreateRoomRequest(settings, autojoin, roomToLeave);
    }
    public static void SendCreateRoomRequest(this SmartFox smartfox, MMORoomSettings settings, bool autojoin = true, Room roomToLeave = null)
    {
        smartfox.Send(new CreateRoomRequest(settings, autojoin, roomToLeave));
    } 
    #endregion

    public static void SendExtensionRequest(this SmartFox smartfox, string command, ISFSObject data, Room room = null, bool useUDP = false )
    {
        smartfox.Send(new ExtensionRequest(command, data, room, useUDP));
    }

    public static void SendPublicMessageRequest(this SmartFox smartfox, string message, ISFSObject parameters = null, Room room = null)
    {
        smartfox.Send(new PublicMessageRequest(message, parameters, room));
    }
    #endregion
}

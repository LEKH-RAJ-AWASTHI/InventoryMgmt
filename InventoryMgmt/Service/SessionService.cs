//using Newtonsoft.Json;

//namespace InventoryMgmt;

//public class SessionService
//{
//    public static SessionVariable GetCurrentUser(HttpContext httpContext)
//    {
//        var sessionValue = httpContext.Session.GetString(SessionVariableEnum.CurrentUser);

//        if (sessionValue is not null)
//        {
//            return JsonConvert.DeserializeObject<SessionVariable>(sessionValue);
//        }
//        return null;
//    }
//}

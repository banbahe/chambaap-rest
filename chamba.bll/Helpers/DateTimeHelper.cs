using System;

namespace chambapp.bll.Helpers
{
    public  class DateTimeHelper
    {
        /*
         *
         * // Make the class generic
public class ResponseObject<T> {
    public T Data { get; set }
    public Boolean Success { get; set; }
    public string Message { get; set; } 
}

// Use generic methods to access the property
public class ResponseObject {
    private object data;
    public T GetData<T>() {
        return (T)data;
    }
    public void SetData<T>(T newData) {
        data = newData;
    }
    public Boolean Success { get; set; }
    public string Message { get; set; } 
}
         */
        // public T CurrentTime { get; set; }

        // public static T CurrentTimestamp<T>()
        // {
        //     object currentTime = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        //     return (T) currentTime;
        // }
        public static int CurrentTimestamp()
        {
            var  currentTime = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            return (int) currentTime;
        }


    }
}
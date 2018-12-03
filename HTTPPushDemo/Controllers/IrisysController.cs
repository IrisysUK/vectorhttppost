
namespace HTTPPushDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// Web API controller to handle HTTP post from devices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IrisysController : ControllerBase
    {
        /// <summary>
        /// Handles data being posted from the device
        /// </summary>
        /// <param name="value">JSON containing the data</param>
        [HttpPost("count_data")]
        public void CountData([FromBody] JToken value)
        {
            // Get the serial number of the device sending the data
            var deviceId = value["FriendlyDeviceSerial"];

            Console.WriteLine("Got data for device " + deviceId);

            // Loop around each count log entry
            foreach (var countData in value["CountLogs"])
            {
                // Extract the log entry ID and the timestamp for when the log entry occured (Note all timestamps are UTC)
                var logEntryID = (long)countData["LogEntryId"];
                var timestamp = (string)countData["Timestamp"];

                Console.WriteLine("Log entry: " + logEntryID + " at time: " + timestamp);

                // Loop around each count value in this entry
                foreach (var count in countData["Counts"])
                {
                    // Name of the register the data is associated with
                    var registerName = (string)count["Name"];

                    // ID of the register
                    var registerID = (long)count["RegisterId"];

                    // Count value
                    var countValue = (long)count["Value"];

                    Console.WriteLine("Register: " + registerName + " ID: " + registerID + " value: " + countValue);
                }
            }

            Console.WriteLine("Data complete");
        }
    }
}

using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking.Business.Business.Interface;
using Parking.Model.ParkingArea;
using Parking.Model.VehicleParking;

namespace ParkingApp.WebApi.Controllers
{

    [ApiController]

    [Route("api/[controller]")]
    public class DashboardApiController : ControllerBase
    {
        private IParkingSpaceBusiness iparkingSpaceBusiness;
        string connectionString = "Endpoint=sb://mssageservicebus.servicebus.windows.net/;SharedAccessKeyName=mssageservicebuspolicy;SharedAccessKey=vkmCaKT3bS0KTvH98reBkUHK7BEH/QnNV+ASbDsIGlo=;EntityPath=orderqueue";
        string queueName = "orderqueue";
        public DashboardApiController(IParkingSpaceBusiness iparkingSpaceBusiness)
        {
            this.iparkingSpaceBusiness = iparkingSpaceBusiness;
        }
        [HttpGet("[action]")]
        public List<ParkingArea> GetAllParkingSlot(int Id)
        {
            return iparkingSpaceBusiness.GetAllParkingSlot(Id);
            
        }
        [HttpPost("[action]")]
        public async Task<string> BookParkingSpace(VehicleParking obj)
        {
            var msg = "";
            if (obj.Id > 0)
            {
                ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
                ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);
                ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
                int i = 0;


                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(obj));
                serviceBusMessage.ContentType = "application/json";
                

                serviceBusMessage.TimeToLive = TimeSpan.FromSeconds(30);
                Random getrandom = new Random();

                serviceBusMessage.MessageId = getrandom.Next(1, 10000000).ToString();
                i++;
                if (!serviceBusMessageBatch.TryAddMessage(serviceBusMessage))
                {
                    throw new Exception();
                }
                ServiceBusClient serviceBusClient_ = new ServiceBusClient(connectionString);
                ServiceBusReceiver serviceBusReceiver_ = serviceBusClient_.CreateReceiver(queueName,
                    new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });


                //Below code Lock untill the message is not processed and for single message
                ServiceBusReceivedMessage message = await serviceBusReceiver_.ReceiveMessageAsync();
                VehicleParking oo = JsonConvert.DeserializeObject<VehicleParking>(message.Body.ToString());
                //TempData["Message"] = "Message recieved and delete successfully";
                //ViewData["OrderID"] = oo.OrderID;
                //ViewData["Quantity"] = oo.Quantity;
                //ViewData["UnitPrice"] = oo.UnitPrice;
                //Order obj = new Order();
                //obj.OrderID = oo.OrderID;
                //obj.Quantity = oo.Quantity;
                //obj.UnitPrice = oo.UnitPrice;
                //obj.Importance = oo.Importance;
                //return View("Index", obj);

            }
            return msg;
        }
    }
}

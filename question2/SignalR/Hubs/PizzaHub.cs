using System.Dynamic;
using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()



        {

         
 await base.OnConnectedAsync();
            _pizzaManager.AddUser();

            Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers); 

         
           


            //await Clients.Caller.SendAsync("")
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser(); 
            await base.OnConnectedAsync();
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId)
            var groupName = _pizzaManager.GetGroupName(choice);
            var nbPizzaPrice = _pizzaManager.GetPizzaPrice(choice); 
            var nbPizza = _pizzaManager.GetNbPizzas(choice);
            var totalArgent = _pizzaManager.GetMoney(choice); 



            

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("UpdatePizzaPrice", nbPizzaPrice); 
        
            await Clients.Caller.SendAsync("UpdateNbPizzasAndMoney", nbPizza, totalArgent); 

              
            
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            var groupName = _pizzaManager.GetGroupName(choice);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        }

        public async Task AddMoney(PizzaChoice choice)
        {

         
            _pizzaManager.IncreaseMoney(choice);

             var groupName = _pizzaManager.GetGroupName(choice);
            var getMoney = _pizzaManager.GetMoney(choice);


         


            Clients.Group(groupName).SendAsync("UpdateMoney", getMoney); 



        }

        public async Task BuyPizza(PizzaChoice choice)
        {

            var groupName = _pizzaManager.GetGroupName(choice);
            _pizzaManager.BuyPizza(choice);



            var nbpizzas = _pizzaManager.GetNbPizzas(choice); 

            var money = _pizzaManager.GetMoney(choice); 

         

            Clients.Group(groupName).SendAsync("UpdateNbPizzasAndMoney", nbpizzas, money); 



        }
    }
}

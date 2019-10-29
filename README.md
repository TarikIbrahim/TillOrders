## TillOrders API doc
Till Orders API supports Swagger by running **http://{HOST}/swagger.index.html**

### API methods ###
**Create Order**
- POST /api/v1/order/create
```
{
	"OrderName":"Office Lunch",
	"Amount":0,
	"OrderItems":[
	{
		"ItemName":"Item5",
		"Price":20,
		"Quantity":4
	},
	{
		"ItemName":"Item2",
		"Price":15,
		"Quantity":12
	},
	{
		"ItemName":"Orange Juice",
		"Price":5,
		"Quantity":2
	}
	]
}
```

**Confirm Order**
- POST /api/v1/order/confirm?orderId={**orderId**}

**Get order details**
- Get /api/v1/order/{**id**}

**Get orders**
- Get /api/v1/orders/all?status={**status**}
status is a numeric values ranges from 1-3 (e.g 1 to get all orders,2 get only paid orders and 3 to get only non-paid orders

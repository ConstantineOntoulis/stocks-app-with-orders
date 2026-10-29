const tokenElement = document.querySelector("#FinnhubToken");
const symbolElement = document.querySelector("#StockSymbol");

if (tokenElement && symbolElement) {

    const token = tokenElement.value;

    const stockSymbol =
        symbolElement.value.trim();

    const socket =
        new WebSocket(`wss://ws.finnhub.io?token=${token}`);

    socket.addEventListener("open", function () {

        socket.send(JSON.stringify({
            type: "subscribe",
            symbol: stockSymbol
        }));

    });

    socket.addEventListener("message", function (event) {

        const eventData =
            JSON.parse(event.data);

        if (eventData.type === "error") {

            console.error(
                "Finnhub WebSocket error:",
                eventData.msg
            );

            return;
        }

        if (
            eventData.type !== "trade" ||
            !eventData.data ||
            eventData.data.length === 0
        ) {
            return;
        }

        const trade =
            eventData.data[eventData.data.length - 1];

        if (trade.s !== stockSymbol) {
            return;
        }

        const price = Number(trade.p);

        if (!Number.isFinite(price) || price <= 0) {
            console.warn("Invalid stock price received:", trade.p);
            return;
        }

        const priceElements =
            document.querySelectorAll(".live-price");

        priceElements.forEach(function (element) {

            element.textContent = price.toFixed(2);

        });

        const orderPriceElement =
            document.querySelector("#order-price");

        if (orderPriceElement) {
            orderPriceElement.value = price.toFixed(2);
        }
        
        const quantityElement =
            document.querySelector("#live-quantity");

        if (
            quantityElement &&
            trade.v !== undefined
        ) {
            quantityElement.textContent =
                trade.v;
        }
    });

    function unsubscribe() {

        if (socket.readyState === WebSocket.OPEN) {

            socket.send(JSON.stringify({
                type: "unsubscribe",
                symbol: stockSymbol
            }));

        }
    }

    window.addEventListener(
        "pagehide",
        function () {

            unsubscribe();

            socket.close();

        }
    );
}
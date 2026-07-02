# IoT Device Simulator 

A C# desktop tool that simulates physical IoT devices. It generates and sends dynamic JSON telemetry data to an MQTT broker, allowing you to stress-test backend systems without needing actual hardware.

**Key Features:**
* **MQTT Ready:** Uses `MQTTnet` for lightweight and fast messaging.
* **Dynamic Data:** Automatically injects real-time timestamps and unique MAC addresses into your JSON templates.
* **Stress Testing:** Asynchronous architecture to simulate multiple devices sending data concurrently.

**How to Use:** 
Update the Broker IP in the code, build the project in Visual Studio, run the `.exe`, and start publishing data!

Light Switch project using the Meridian/P arm hardware and Micro Framework V4 
as a high-tech sensor laden network based 

------------------------------------------------------------------------------------------------
Requirements:
------------------------------------------------------------------------------------------------
Required VS2008 SP1 (VS2010 won't work with MF 4).

.Net MicroFramework V4.1
 (http://www.microsoft.com/downloads/details.aspx?FamilyId=77dbfc46-14a1-4dcf-a809-eda7ccfe376b&displaylang=en)

Device Solutions SDK (V4.1) (http://devicesolutions.net/Support/Downloads.aspx)

Developed working against the Meridian/P (http://devicesolutions.net/Products/MeridianP.aspx) and ethernet adapter.


------------------------------------------------------------------------------------------------
What it does:
------------------------------------------------------------------------------------------------
Act as a touch screen light switch with added sensors and interfacing.

Ethernet based networking to allow:
----------------------------------
* Control of home automation system (i.e. the lights!) over TCP/IP
** via a centeral server (i.e. home server)
** via non-centeralised fixed IP addressing

* The device should be powered from POE, or from an external source (USB) whilst in dev.

* Device will be able to get currenct date/time from a NNTP server.

* Device will be controllable via a api
** i.e. provide mobile phone based control
** provide JQuery based webpage to read sensors.

* Twitter integration, send/receive tweets
** i.e. send tweet if motion sensor triggered during a certain time..


Array of Sensors to allow:
-------------------------
* Audio
** Record/stream microphone in Startrek style communication, possibly to centeral server for speach to action processing.
** Play audio - such as a wake up alarm, notification etc.  May be linked to an external TCP/IP controlled sound system.

* Humidity - record the room humidity

* IR
** Receive IR signals from remote controls
** Send IR signals to control equipment in the room (e.g. IR controlled fan)

* LED's
** abilty to provide low level of lighting from LED's for us in guidance during powerfail 
** provide guidance to allow location of the switch
** provide dim lighting for getting to bed/ wakeup.

* Light - measure lightlevel in the room.

* Movement - Detect movement around the sensor.

* RF - 433.92MHz comms to allow local shortrange transmittion/reception of HomeEasy devices and other 433 based devices.

* RFID - allow simple reading of RFID cards to activate specific lighting plans.

* Temperature - measure the room temperature.

All sensors should be optional, not all sensors will be possible to fit into a lightswitch format device.

User interface:
--------------
* Touch screen with simple press to switch on/off light, basic key design.
* key sequence to get to menu/options.

import sys, time, os, requests, json, syslog
from requests.adapters import HTTPAdapter
from threading import Thread
import piface.pfio as pf
"""  
Pi Client module for On-The-Fly and Cabins. 
"""
url_OTF = "http://192.168.1.242/otf/api/reservation/cardswipe"

os.chdir('/home/pi/')
print "\n+++++++++++++  piclient Version: 6.3  +++++++++++++\n"

button_press_wait_time = 30.0
pfinitalized = False
def p(*msg):
	print msg
	fmsg = ""
	for e in msg:
		fmsg = fmsg + ", " + str(e)
	#syslog.syslog(syslog.LOG_ERR, fmsg)
	
def server_call(card_swipe_data, button_index):
	global url_OTF
	try:
		p( card_swipe_data, button_index, url_OTF )
		r = requests.get( url_OTF, params={'cardswipedata':card_swipe_data, 'buttonindex':button_index} )  # for post use data=pars
		rjson = r.json()
		p('>>>>------------>', rjson, '<==========<<<<')
		return rjson
	except requests.ConnectionError, e: #requests.exception.RequestException as e:
		p( "::::: Exception in requests :::::", e )
	return None
def button_pressed(): # Note:PiFace Buttons(index) are switchable.
	p("button press check called ++++++++++++++")
	for button_index in range(0, 8):
		if pf.digital_read(button_index):
			return button_index
	return -1
def wait_for_button_press(card_swipe_data):
	global pfinitalized
	p("Thread started---------")
	if False == pfinitalized:
		pf.init()
		p("pf initialized<-----------------")
		pfinitalized = True
	end_time = time.time() + button_press_wait_time
	while time.time() < end_time:
		button_index = button_pressed()
		p("-After-", str(button_index), time.time())
		if button_index > -1:
			server_call(card_swipe_data, button_index)
			button_index = -1
			break
		time.sleep(0.1)
	p("---------------Thread Ended---------------")
def start_app(mainargs):
	while(True):
		p("piclient++++++++++++++++++++++piclient.start_app() called- 12")
		card_swipe_data = raw_input() #sys.stdin.readline()	 
		p( ">>>-------->", card_swipe_data )
		if card_swipe_data == 'exit':
			sys.exit(0)
		else:
			if card_swipe_data.startswith("C"): # Cabinets starts with C
				button_thread = Thread(target=wait_for_button_press, args=(card_swipe_data,))
				button_thread.start()
			else:
				server_call(card_swipe_data, -1)
#start_app(sys.argv)
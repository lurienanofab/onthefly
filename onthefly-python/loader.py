import os, sys, requests
print "\n------***-----Loader Version 8.6------***-----\n"
urlCodeLoader = "http://192.168.1.242/otf/api/clientcode/getlatestcode?filename="
os.chdir('/home/pi/')

def get_file(saveFileName, urlstr=urlCodeLoader):
	r = get_url(urlstr + saveFileName)
	if r.status_code != 200: # on error dont update
		return r
	rnew = r.text.replace('\r\n', '\n')
	fil = open(saveFileName, 'w')
	fil.write(rnew)
	fil.close()
	return r
def get_url(urlstr, pars=None):
	r = requests.get(urlstr, params=pars)
	if r.status_code != 200: # if first call fails.
		print "[2] First time:status code failed:-" + str(r.status_code)
		r = requests.get(urlstr)
	print "[1] Status code:-" + str(r.status_code), urlstr
	return r
def run_loader():
	args = sys.argv	
	lftp = False
	if len(args) > 1:
		if args[1] == "url":
			lftp = True
	if lftp:
		saveFileName = args[3]
		get_file(args[2], args[3])
	else:
		get_file("loader.py") #update loader.py itself first
		get_file("piclient.py")
		import piclient
		piclient.start_app(sys.argv)
run_loader()
#print "\n", " ++++++++ENDED+++++++\n"
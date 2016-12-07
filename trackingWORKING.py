import cv2
import numpy as np
import time
import socket

UDP_IP = "127.0.0.1"
UDP_PORT = 5005
UDP_PORT2 = 5004

print "UDP target IP:", UDP_IP
print "UDP target port:", UDP_PORT
print "UDP target port2:", UDP_PORT2

sock = socket.socket(socket.AF_INET, # Internet
                     socket.SOCK_DGRAM) # UDP

time.sleep(2)
                        
cap = cv2.VideoCapture(0)

time.sleep(2)

#sets webcams' resolution
cap.set(cv2.cv.CV_CAP_PROP_FRAME_WIDTH, 800)
cap.set(cv2.cv.CV_CAP_PROP_FRAME_HEIGHT, 500)

time.sleep(2)

    #defines the contours
def findanddraw(_contours):
    
    contours = _contours
    
    for c in contours:r
        areas = [cv2.contourArea(c) for c in contours]
        
        # finds the biggest contour
        max_index = np.argmax(areas)
        cnt = contours[max_index]
    
        #finds the circle around the contour
        (x,y),radius = cv2.minEnclosingCircle(cnt)
        center = (int(x),int(y))
        radius = int(radius)
        
        #draws the circle around the contour
        cv2.circle(frame,center,radius,(0,255,0),2)
        
                #finds center of the biggest contour
        M = cv2.moments(cnt)
        
        if M['m00'] != 0:         
            cx = int(M['m10']/M['m00'])
            cy = int(M['m01']/M['m00'])
            #draws the mieddle of a contour        
            #cv2.circle(frame, (cx, cy), 5, (255, 255, 255), -1)
            
            return str(cx)+","+str(cy)
        
while(1):

    # Take each frame
    _, frame = cap.read()
    frame = cv2.medianBlur(frame,5)
    #Flips screen upsidedown
    frame=cv2.flip(frame,0)
    # Convert BGR to HSV
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)

    # define range of blue color in HSV
    
    #FIRST COLOURS
   # lower_green = np.array([35,115,140])
   # upper_green = np.array([45,140,210])
    
    #lower_blue = np.array([110,150,50])
    #upper_blue = np.array([140,254,254])
    
    #lower_red = np.array([0,150,50])
    #upper_red = np.array([5,254,150])
    
    #lower_redup = np.array([175,150,50])
    #upper_redup = np.array([180,254,150])
    
    
    #NEW COLOURS
    # Meassured Colour: 111, 229.5, 107.1
    #lower_blue2 = np.array([75,225,50])
    #upper_blue2 = np.array([120,235,180])
    
    # Meassured Colour: 152, 122.4, 117.3
    lower_purple = np.array([120,45,25])
    upper_purple = np.array([145,255,255])
    
    # Meassured Colour: 101, 209.1, 91.8
    #lower_greenIsh = np.array([95,155,45])
    #upper_greenIsh = np.array([106,245,165])
    
    # Meassured Colour: 174, 226.9, 170.8
    #lower_red2 = np.array([170,220,165])
    #upper_red2 = np.array([180,230, 175])
    
    # Meassured Colour: 165.5, 137.7, 198.9
    lower_pink = np.array([155,90,90])
    upper_pink = np.array([175,170,255])
    
    

    # Threshold the HSV image to get only BLUE colors
    #mask = cv2.inRange(hsv, lower_blue, upper_blue)
    mask = cv2.inRange(hsv, lower_pink, upper_pink)

    
    # Threshold the HSV image to get only RED colors
    #maskREDLOW = cv2.inRange(hsv, lower_red, upper_red)
    #maskREDUP = cv2.inRange(hsv, lower_redup, upper_redup)
    mask2 = cv2.inRange(hsv, lower_purple, upper_purple)

    contours, hierarchy = cv2.findContours(mask,1,2)

    contours2, hierarchy = cv2.findContours(mask2,1,2)

    #contours2, hierarchy = cv2.findContours(maskREDLOW + maskREDUP,1,2)
    
    #print "message:", str(findanddraw(_contours = contours))
    sock.sendto(str(findanddraw(_contours = contours)) , (UDP_IP, UDP_PORT))
    #print 'object 1', findanddraw(_contours = contours)
    #print findanddraw(_contours = contours)
    
    sock.sendto(str(findanddraw(_contours = contours2)) , (UDP_IP, UDP_PORT2))
    #print 'object 2', findanddraw(_contours = contours2)
    
    
    cv2.imshow('frame',frame)
    #cv2.imshow('mask',mask)

    #cv2.imshow('maskRED', maskREDLOW + maskREDUP)

    k = cv2.waitKey(5) & 0xFF
    if k == 27:
        break

cv2.destroyAllWindows()
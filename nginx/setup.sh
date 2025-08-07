#!/bin/bash
cp ./kestrel-itsm.service /etc/systemd/system/kestrel-helloapp.service
systemctl enable kestrel-itsm.service
systemctl start kestrel-itsm.service
systemctl status kestrel-itsm.service
systemctl enable nginx
systemctl start nginx
systemctl status nginx
#!/bin/bash
setenforce 0
cp ./kestrel-itsm.service /etc/systemd/system/kestrel-itsm.service
systemctl enable kestrel-itsm.service
systemctl start kestrel-itsm.service
systemctl status kestrel-itsm.service
systemctl enable nginx
systemctl start nginx
systemctl status nginx

sudo ausearch -m avc -ts recent | sudo audit2allow -M itsm-dotnet-policy
sudo semodule -i itsm-dotnet-policy.pp

sudo firewall-cmd --permanent --zone=public --add-port=5050/tcp
sudo firewall-cmd --reload
sudo firewall-cmd --list-ports

systemctl restart kestrel-itsm.service
systemctl restart nginx
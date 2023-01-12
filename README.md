# adminis_test
𝐝𝐨𝐜𝐤𝐞𝐫-𝐬𝐞𝐭𝐮𝐩.𝐬𝐡
#!/bin/bash

echo "* Add hosts ..."
echo "192.168.99.100 docker.lab docker" >> /etc/hosts

echo "* Add any prerequisites ..."
apt-get update
apt-get install -y ca-certificates curl gnupg lsb-release

echo "* Add Docker repository and key ..."
mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
$(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

echo "* Install Docker ..."
apt-get update
apt-get install -y docker-ce docker-ce-cli containerd.io docker-compose-plugin

echo "* Install Git ..."
apt-get update
apt-get install git

echo "* Install .net ..."
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

apt-get update
apt-get install -y dotnet-sdk-6.0

sudo apt-get update
apt-get install -y aspnetcore-runtime-6.0

echo "* Install docker-compose ..."
curl -L "https://github.com/docker/compose/releases/download/1.26.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose

echo "* Create directory for project ..."
mkdir /etc/adminis_test
cd /etc/adminis_test

echo "* Clone repo ..."
git clone https://github.com/Kvazark/adminis_test.git

#Start compose
cd /etc/adminis_test/adminis_test

docker-compose build
docker-compose up

echo "* Add vagrant user to docker group ..."
usermod -aG docker vagrant
𝐕𝐚𝐠𝐫𝐚𝐧𝐭𝐟𝐢𝐥𝐞
Vagrant.configure("2") do | config |
    config.vm.box = "ubuntu/jammy64"
    config.vm.boot_timeout = 600
    config.vm.define "docker" do |docker|
        docker.vm.hostname = "docker.lab"
        #docker.vm.network "private_network", ip: "192.168.99.100"
        docker.vm.network "forwarded_port", guest: 5050, host: 80, auto_correct:true
        docker.vm.provision "shell", path: "docker-setup.sh"
        docker.vm.provider "virtualbox" do |vb|
            vb.customize ["modifyvm", :id, "--memory", "2048"]
        end
    end    
end

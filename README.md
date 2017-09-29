# To setup the project:


## Steps to get the project on your machine

0. Get GitBash if you do not have it on **Windows**
1. Navigate/Create the folder that you want to hold your Unity Projects
2. Fork the GitHub Repository so you have a personal copy
3. Clone your forked repo: git clone **ht<span>tps://github.com/(your username)/team10_hgd.git**
4. Once you have it cloned, Navigate into folder that was created (should be team10_hgd): 
* git remote add (your username) **ht<span>tps://github.com/(your username)/team10_hgd.git**
* git remote set-url origin **ht<span>tps://github.com/ronaldliu/team10_hgd.git**
5. Open Unity
6. Open the folder that you cloned
- There you go you should have everything setup
- Unity might have to create somethings

##Development Workflow

1. Find task to do
2. Create an issue for the task (Title, Short Description, Assign it to yourself)
3. Make sure you are on master on your machine. (**git checkout master**)
4. Make sure master is up to date. (**git fetch origin** -> **git merge origin/master**)
5. Create a branch for your issue. (**git checkout -b (name of issue)**)
6. Do work...
7. When done... 

**OR if checkpoint, just don't merge**
* Highly recommend creating a pull-request before you leave to do other things, so that you have something to refer back to incase you forget your branch name
8. **git add -A**
9. **git commit -m "(insert message here)"**
10. **git push (name of your remote) (name of branch)**
11. Go into GitHub -> Pull-Request -> Create a Pull-Request **OR** Do it from the branch menu **OR** in the code section at the top


###IMPORTANT: Try to make sure that 2 people are not working on the same thing at the same time

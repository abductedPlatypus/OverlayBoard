# OverlayBoard
This is an unfinished project. Below is a log of what is currently required to get the project up and running in its unfinished state.
## Setup
Go to http://www.twitch.tv/kraken/oauth2/clients/new and register your app.  Use ``http://localhost:3000/_oauth/twitch?close`` or similar for your website location.
Update the ``settings.json`` in the embed folder (e.g. ``~/OverlayBoard/embed``) and update it with the cient id and secret provided by Twitch. Make sure to keep these codes private (it's called secret for a reason).
```
{
    "twitch":{
        "clientId":"myw31rdcl13nt1d",  // replace with the 
        "secret":"my3v3nw13rd3rs3cr3t" // codes given by twitch
    }
}
```

Launch with:  
``cd <embed folder location e.g. cd ~/OverlayBoard/embed>``  
``meteor --settings settings.json``

__If you plan to host your codes on a (public) repository__, rename your ``settings.json`` to ``private_settings.json`` and launch with the same settings filename. This name is added to the .gitignore so it will not be uploaded to your respository.

__When you want to host this meteor project__, update everything on both Twitch and in your settings.json accordingly. I would even recommend registering your application twice, so you have development and production version of your codes.
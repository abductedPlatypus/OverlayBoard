# OverlayBoard
This is an unfinished project. Below is a log of what is currently required to get the project up and running in its unfinished state.
## Setup
Go to http://www.twitch.tv/kraken/oauth2/clients/new and register your app.  Use ``http://localhost:3000/_oauth/twitch?close`` or similar for your website location.
Create a settings.json in the embed root and fill it out with the cient id and secret provided by Twitch.
```
{
    "twitch":{
        "clientId":"myw31rdcl13nt1d", // replace with your own settings
        "secret":"my3v3nw13rd3rs3cr3t"
    }
}
```

Launch with:  
``cd <embed folder location e.g. cd ~/OverlayBoard/embed>``  
``meteor --settings settings.json``
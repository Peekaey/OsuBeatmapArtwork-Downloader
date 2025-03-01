# Osu Beatmap Artwork Downloader
Download Osu beatmap artwork the easy way!

Automates the process of downloading beatmaps and extracting the artwork. Only keeping the files that YOU wish to keep.


### How it works.
1. User provides their login session cookie direct from the osu site (This can be obtained via the Network tab in the browser dev tools.)
2. User enters the beatmapset Id into the application.
3. Application will download the beatmapset in the background, load any images found in the beatmapset to memory and delete the beatmapset automatically.
4. Images are displayed to the user with the option to save any desired image in original quality.


### FAQ
- **Q:** Why do I need to provide my session cookie from the osu site?
  - **A:** As you may or not be aware, being logged into the Osu site is required to download beatmaps. There is the option of using third
  party mirror sites to download the beatmaps without being authenticated, however they do not have all the beatmaps that are currently available from official sources.
    -  There is an official API Endpoint that can be authenticated with OAuth which is found at the [osu!api v2 documentation](https://osu.ppy.sh/docs/index.html#get-apiv2beatmapsetsbeatmapset) , however this endpoint is currently only available to the Osu Lazer Client.
    - Once this endpoint is publicly available can the auth method can be changed to one that is more desireable.
- **Q:** Do I need to enter my session cookie each time?
- **A:** The session cookie needs to be provided each time the application is started as the cookie is not stored by default. Tthere is an option in the settings to save this cookie 
  to a json config file for the application to read and load automatically on startup. 


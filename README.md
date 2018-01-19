# PoolApiToCSV
Gets data from mining pools every interval minutes and writes it to CSV file.

I've made a small C# program (as I'm more proficient with that than with powershell) that gets data from pool's apis every 2 minutes. I've made it configurable to so anyone can use it.

You'll have to edit app.config (or PoolApiToCSV.exe.config in debug folder) for it to work for your wallet data, just replace the wallet address at the end of each API address.
You can also use it for other APIs, just make sure you correlate measures with api addresses, and refer to each pool's api page (eg. https://www.ahashpool.com/site/api/ )

If you want to open the CSV with Excel and you still have the tool open, make a copy of the CSV file and open that as excel locks the file.
In order to make graphs in Excel, the most useful and easy thing is to select all data and insert a scatter X, Y graph, there you can compare your actual profits made on different pools (or anything else you have configured), by having the DateTime as your X axis, like that, even if you don't keep this tool running all the time or you change the interval, you'll still see relevant data.

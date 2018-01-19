# PoolApiToCSV
Gets data from mining pools every interval minutes and writes it to CSV file.

I've made a small C# program (as I'm more proficient with that than with powershell) that gets data from pool's apis every 2 minutes. I've made it configurable to so anyone can use it.

You'll have to edit app.config (or PoolApiToCSV.exe.config in debug folder) for it to work for your wallet data, just replace the wallet address at the end of each API address.
You can also use it for other APIs, just make sure you correlate measures with api addresses, and refer to each pool's api page (eg. https://www.ahashpool.com/site/api/ )

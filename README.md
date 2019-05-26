# AquariusIntelLoader
This project is designed to be able to take an Intel .HEX file and either convert it into the classic READ/DATA basic app that can be copied and pasted into Virtual Aquarius or create the classic Basic Bootstrap loader and machine code file as an Aquarius .CAQ file for Virtual Aquarius

If the code is assembled so that an $org address is given in the .HEX file then the bootstrapper will provide relocation service as well, other wise it will default to relocating to $4000 (16384)

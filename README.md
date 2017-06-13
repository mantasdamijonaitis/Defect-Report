# Defect-Report - KTU duomenų bazių laboratorinis darbas
.NET Core aplikacija ir duomenų bazė kurta KTU Duomenų Bazių moduliui.

Iš pirmo atsiskaitymo gavau 9, antro ir trečio - 10.

Duomenų bazės dalykinis aprašas: 

Duomenų bazė registruoti konstrukciniams defektams.
Ši duomenų bazė skirta registruoti konstrukciniams defektams, leidžia priskirti asmenis bei transportą defekto ištaisymui. Sėkmingui darbui su sistema, turi būti įvedamos medžiagos, sistemoje saugomas jų kiekis, pavadinimas, bei matavimo vienetas. Matavimo vienetai gali būti: kg, m, m2, m3. Defektui taisyti yra priskiriamas darbuotojas, kurio vardas ir pavardė yra saugomi sistemoje, ir jis nebūtinai yra Informacinės Sistemos naudotojas. Informacinės sistemos naudotojas privalo turėti prisijungimo vardą, užšifruotą slaptažodį, bei turi būti indikatorius, ar leisti šiam vartotojui prisijungi prie sistemos. Kiekvienas vartotojas privalo turėti rolę, ir priklausyti kokiai nors įmonei. Galimos darbuotojų rolės: konstruktorius, vairuotojas, vadovas. Defektą užregistravęs, bei šalinimą priskyręs darbuotojai būtinai turi būti Informacinės sistemos naudotojai.  Tai gali būti arba konstruktorius, arba vadovas. Defektas šalinamas į statybų aikštelę atvežant medžigą,priskiriant vieną konstruktorių arba vadovą defekto šalinimui, ir vieną vairuotoją atvežimui. Medžiagas veža arba paprastas automobilis, arba vilkikas su priekaba (šiuo atveju pažymimi tik priekabos valstybiniai numeriai). Transporto priemonės bei darbuotojai priskiriami įmonei, kuri turi pavadinimą, kodą, bei tipą. Galimi įmonių tipai: vežėjas, rangovas, vykdytojas. Defektas registruojamas vienoje statybų aikštelėje, kuriai yra priskiriamas adresas, bei po vieną įmonę su atributu vežėjas, rangovas, bei vykdytojas. Taip pat registruojant defektą, yra pažymima jo registravimo data ir laikas, terminas, iki kada turi būti pataisytas defektas, bei požymis, ar defektas jau ištaisytas. Norint vizualiai pavaizduoti defektą, jam gali būti priskiriama viena ar daugiau nuotraukų, kur kiekvienai iš jų priskiriamas nuotrauką įkėlęs darbuotojas, nuotraukos įkėlimo data bei vieta sistemos fizinėje atmintyje.

Duomenų bazės schema: 

![alt text](https://github.com/mantasdamijonaitis/Defect-Report/blob/master/sqls/MantasDamijonaitis-IFF-5-4-Schemos-Screenshot.PNG?raw=true)

Antram laboratoriniui įgyvendinti formų tipai: PNLF1, PNLF2, PPLF1, PPLF2, SPLFv, SPLFa formų tipai.

Trečiam laboratoriniui įgyvendinti formu tipai: AA1, AA2, G4

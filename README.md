Můj program jsem navrhla tak, aby sledoval stav počasí u krajských měst ČR.
Výsledný výpis obsahuje aktuální datum a čas zpracování, teplotu, rychlost větru a stav oblačnosti pro každé město zvlášť.
Na závěr se všechny údaje automaticky zprůměrují na celkovou průměrnou teplotu, průměrnou rychlost větru a převažující oblačnost v České Republiky.
Při návrhu jsem se zaměřila na to, aby byl program přehledný a snadno rozšiřitelný. 
Kód jsem rozdělila na menší části tak, aby se každá třída starala jen o jeden konkrétní úkol, například o stahování dat nebo o jejich výpočet.
Využila jsem rozhraní, díky kterému můžeme v budoucnu jednoduše vyměnit zdroj počasí, aniž by se musel zbytek aplikace předělávat.
Celý program běží asynchronně, takže zbytečně nezatěžuje systém při čekání na odpověď z internetu. 
Vytvořila jsem také vlastní systém chyb, který dokáže srozumitelně nahlásit výpadek sítě nebo nenalezení města.
Aby program při problémech s připojením nezamrzl, nastavila jsem u všech požadavků časové limity.
Tímto postupem jsem dosáhla toho, že je aplikace stabilní a velmi jednoduchá na údržbu.

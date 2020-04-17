# VenditaVeicoliSoluction 
Applicazione Windows Form che permette la gestione di un salone di un salone vendita auto e moto nuove e usate.

### Attenzione! Sono già stati caricati dati fittizi per esempio

## Classi
Le classi presenti sono:
- **SerializableBindingList**: BindingList serializzaile
- **Veicolo**: abstract, possiede i campi per descrivere un veicolo generico
  - **Auto**: subclasse
  - **Moto**: subclasse
- **Vendita**: Contiene i dati relativi ad una vendita di un veicolo
- **Storico**: Contiene i dati e i metodi che permettono di elaborare e salvare le vendite recenti
- **Utilities**: Contiene metodi generici utili
- **FileUtils**: Contiene metodi generici per il salvataggio (SERIALIZZAZIONE E PARSIFICAZIONE)
- **VeicoliUtils**: Contiene metodi statici per la generazione di stringe sql facilitando la coostruzione delle più comuni
- **Card**: Oggetto grafico
- **OpenXmlUtils**: Contiene metodi generici per Word Processing
- **AccessUtils**: Contiene metodi generici e statici per interazione con Access DB
- **MySqlUtils**: Contiene metodi generici e statici per interazione con MySql DB
- **SqlServerUtils**: Contiene metodi generici e statici per interazione con SqlServer DB

## SCHEMA DLL
![alt text](https://github.com/abongioanni/VenditaVeicoliSoluction/blob/master/WindowsFormsApp/www/images/DLL.PNG "Schema DLL")

## SCHEMA DB
![alt text](https://github.com/abongioanni/VenditaVeicoliSoluction/blob/master/image.png "Schema DB")

## Form

### Attenzione! La form quando si chiude dovrebbe cancellare le foto di elementi eliminati... ma non funziona!

La Form è strutturata con un ToolMenuStrip con i seguienti elementi:
- **Logo**: Bottone che apre il sito del gestore
- **Nuovo**: Apre una tab con i controlli per aggiungere un nuovo veicolo
 ```C#
//Campi richiesti dalla classe Veicolo:
string targa;
string marca;
string modello;
int cilindrata;
double potenzaKw;
DateTime immatricolazione;
bool isUsato;
bool isKmZero;
int kmPercorsi;
string colore;
double prezzo;

int NAB; //Numero airBag SOLO AUTO
string marcaSella; //SOLO MOTO    
```

- **Apri**: Carica i dati salvati nel programma (da file.json, se ci sono errori da csv)
- **Salva**: Salva i dati dei veicoli e dello storico delle vendite su DB AccessDb e MySql
- **Cerca**: Ricerca nei veicoli salvati; apre una tab con i risultati
- **Volantino**: Apre una tab che visualizza una pagina Web; i dati dei veicoli vengono esportati in json su un HTML e visualizzati
- **Impostazioni**: Apre una tab che permette di modificare il testo nell'header e ne footer del sito-volantino (Da migliorare)
- **Cronologia**: Vengono visualizzate in una tab le vendite recenti
- **Chiudi**: Bottone che chiude la tab corrente
- **Export in Word**: Esporta i dati di tutti i veicoli in un file Word; se la tab corrente contiene i risultati di una ricerca stamperà quei risultati
- **Export in Excel**: Esporta i dati di tutti i veicoli in spreadsheet di Excel; se la tab corrente contiene i risultati di una ricerca stamperà quei risultati

### Card
Questo oggetto grafico presenta 2 forme:
1. Il controllo figura come un pannello 300px * 300px con una **PictureBox** per l'immagine,
una **Label** che si auto regola il font (contenente la marca, il modello, il prezzo e lo stato) e un bottone **'Visualizza'**

2. Una tab con tutti i dettagli sulle caratteristiche del veicolo, **l'immagine**, un bottone **Esci'** per chiudere la tab,
un bottone **'Venduto'** per rimuovere il veicolo e metterlo nello storico, un bottone **Stampa'** per stampare le caratteristiche del veicolo. 
C'è anche la possibilità di **modificare** le proprietà del veicolo.


## Console
Questo progetto permette di utilizzare il gestore del salone da una console con la seguente shell:

- -h: elenco comandi
- -a: aggiungi veicolo
- -v [targa]: visualizza dettagli veicolo con la targa [targa]
- -e [targa] [proprietà]: permette di modificare il campo [proprietà] del veicolo [targa]
- -d [targa]: elimina il veicolo con targa [targa]
- -word: esporta l'elenco dei veicoli in un documento word
- -excel: esporta l'elenco dei veicoli in un foglio di excel


Per informazioni contattate Alberto Bongioanni con una mail: a.bongioanni.0746@vallauri.edu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;
using System.IO;


namespace PingMaster4000
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddPing addPing;
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter adapter;
         

        public MainWindow()
        {
            InitializeComponent();

            //Erstellt einen neuen Ping (PingBox) mit der IP-Adresse, dem Intervall in Sekunden und der Dauer fuer die Durchschnittsberechnung in Sekunden
            DataTable DT = GetDataTable("tbl_config","");
            fillPingpanelFromDataTable(DT);
            DT.Dispose();

            //pingStackpanel.Children.Add(new PingBox(ipAddress, 1, 60, 50)); Alt
            //string ipAddress = "8.8.8.8"; Alt

        }
        // DB Funktionen
        private void SetConnection()
        {
            if (!File.Exists("ping.sqlite"))
            {
                MessageBox.Show("Database not Exists!\n New \'ping.sqlite\'.");
                newDb();
            }
            sql_con = new SQLiteConnection("Data Source=ping.sqlite;Version=3;New=False;Compress=True;");
        }

        private int ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            int rowupdated;
            try
            {
                rowupdated = sql_cmd.ExecuteNonQuery();
            }
            catch
            {
                sql_con.Close();
                return 0;
            }

            sql_con.Close();
            return rowupdated;
        }
        
        public DataTable GetDataTable(string tablename,string addSql)
        {
            
            DataTable DT = new DataTable();
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = string.Format("SELECT * FROM {0} {1} ", tablename, addSql);
            adapter = new SQLiteDataAdapter(sql_cmd);
            adapter.AcceptChangesDuringFill = false;
            adapter.Fill(DT);
            sql_con.Close();
            DT.TableName = tablename;
           
            return DT;
          
        }

        public int countDataTable(string tablename, string addSql)
        {
            int count = -1;
            DataTable DT = new DataTable();
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = string.Format("SELECT COUNT(*) AS Anzahl FROM {0} {1} ", tablename, addSql);
            adapter = new SQLiteDataAdapter(sql_cmd);
            adapter.AcceptChangesDuringFill = false;
            adapter.Fill(DT);
            sql_con.Close();
            foreach (DataRow row in DT.Rows)
            {
                count = (int )row.Field<long>("Anzahl");
            }
            return count;

        }


        public void loadPingHistoryFromDataTable(DataTable DT)
        {
            foreach (DataRow row in DT.Rows)
            {
                /*
                 = row.Field<String>("Ping_ID");
                 = row.Field<String>("TTL");
                 = row.Field<String>("Bytes");
                 = row.Field<String>("IP");
                 = row.Field<String>("Status");
                 = row.Field<String>("ping_time");
                 = row.Field<String>("timestamp");
                 */
            }
        }
        
        
        public void fillPingpanelFromDataTable(DataTable DT)
        {
            string ip;
            int interval, configId, range, timeError, autostart;
            

            foreach (DataRow row in DT.Rows)
            {
                autostart = 0;
                configId   = (int) row.Field<long>("Config_ID");
                ip         = row.Field<String>("IP");
                interval   = (int) row.Field<long>("Interval");
                range      = (int) row.Field<long>("Range");
                timeError  = (int) row.Field<long>("TimeError");
                autostart  = (int) row.Field<long>("autostart");

                pingStackpanel.Children.Add(new PingBox(configId, ip, interval, range, timeError, autostart));
                

            }
        }
        
        public void pingCreate(string tbl, int ip,int ttl, int bytes, string status, int ping_time,int timestamp, int autostart)
        {
         
            string txtSqlQuery;
            txtSqlQuery = "INSERT INTO tbl_ping (IP, TTL, Bytes, Status, ping_time, timestamp) ";
            txtSqlQuery += string.Format("VALUES (@{0},@{1},@{2},@{3},@{4},@{5})",                
            ip, ttl, bytes, status, ping_time, timestamp, autostart);

            try
            {
                ExecuteQuery(txtSqlQuery);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void configCreate( string ip, int interval, int range, int timeError, int autostart)
        {
           
            string txtSqlQuery = "INSERT INTO tbl_config (IP, Interval, Range, TimeError, autostart) ";
            txtSqlQuery += string.Format("VALUES ('{0}',{1},{2},{3},{4})",
            ip, interval, range, timeError, autostart);
            

            try
            {
                ExecuteQuery(txtSqlQuery);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void newDb()
        {
            SQLiteConnection.CreateFile("ping.sqlite");
            string sql = @"BEGIN TRANSACTION;
            CREATE TABLE IF NOT EXISTS `tbl_top_level_domain` (
	            `topLevelDomain_ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	            `endung`	TEXT,
	            `beschreibung`	TEXT
            );
            INSERT INTO `tbl_top_level_domain` VALUES (1,'ac','Ascension Island');
            INSERT INTO `tbl_top_level_domain` VALUES (2,'ad','Andorra (Principality of)');
            INSERT INTO `tbl_top_level_domain` VALUES (3,'ae','United Arab Emirates');
            INSERT INTO `tbl_top_level_domain` VALUES (4,'aero','Air-transport industry');
            INSERT INTO `tbl_top_level_domain` VALUES (5,'af','Afghanistan (Islamic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (6,'ag','Antigua and Barbuda');
            INSERT INTO `tbl_top_level_domain` VALUES (7,'ai','Anguilla');
            INSERT INTO `tbl_top_level_domain` VALUES (8,'al','Albania (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (9,'am','Armenia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (10,'an','Netherlands Antilles');
            INSERT INTO `tbl_top_level_domain` VALUES (11,'ao','Angola (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (12,'aq','Antarctica');
            INSERT INTO `tbl_top_level_domain` VALUES (13,'ar','Argentina (Argentine Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (14,'arpa','Address and Routing Parameter Area');
            INSERT INTO `tbl_top_level_domain` VALUES (15,'as','American Samoa');
            INSERT INTO `tbl_top_level_domain` VALUES (16,'asia','Organisations and individuals in the Asia-Pacific region');
            INSERT INTO `tbl_top_level_domain` VALUES (17,'at','Austria (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (18,'au','Australia (Commonwealth of)');
            INSERT INTO `tbl_top_level_domain` VALUES (19,'aw','Aruba');
            INSERT INTO `tbl_top_level_domain` VALUES (20,'ax','Åland Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (21,'az','Azerbaijan (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (22,'ba','Bosnia and Herzegovina');
            INSERT INTO `tbl_top_level_domain` VALUES (23,'bb','Barbados');
            INSERT INTO `tbl_top_level_domain` VALUES (24,'bd','Bangladesh (People''s Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (25,'be','Belgium (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (26,'bf','Burkina Faso');
            INSERT INTO `tbl_top_level_domain` VALUES (27,'bg','Bulgaria (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (28,'bh','Bahrain (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (29,'bi','Burundi (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (30,'biz','Business');
            INSERT INTO `tbl_top_level_domain` VALUES (31,'bj','Benin (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (32,'bl','Saint Barthélemy (Collectivity of) {unassigned - see also: .gp and .fr}');
            INSERT INTO `tbl_top_level_domain` VALUES (33,'bm','Bermuda');
            INSERT INTO `tbl_top_level_domain` VALUES (34,'bn','Brunei (Nation of Brunei - the Abode of Peace) [Negara Brunei Darussalam]');
            INSERT INTO `tbl_top_level_domain` VALUES (35,'bo','Bolivia (Plurinational State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (36,'bq','Caribbean Netherlands [Bonaire - Sint Eustatius and Saba] {unassigned - see also: .an and .nl}');
            INSERT INTO `tbl_top_level_domain` VALUES (37,'br','Brazil (Federative Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (38,'bs','Bahamas (Commonwealth of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (39,'bt','Bhutan (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (40,'bv','Bouvet Island');
            INSERT INTO `tbl_top_level_domain` VALUES (41,'bw','Botswana (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (42,'by','Belarus (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (43,'bz','Belize');
            INSERT INTO `tbl_top_level_domain` VALUES (44,'ca','Canada');
            INSERT INTO `tbl_top_level_domain` VALUES (45,'cat','Catalan');
            INSERT INTO `tbl_top_level_domain` VALUES (46,'cc','Cocos (Keeling) Islands (Territory of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (47,'cd','Congo (Democratic Republic of the) [Congo-Kinshasa]');
            INSERT INTO `tbl_top_level_domain` VALUES (48,'cf','Central African Republic');
            INSERT INTO `tbl_top_level_domain` VALUES (49,'cg','Congo (Republic of) [Congo-Brazzaville]');
            INSERT INTO `tbl_top_level_domain` VALUES (50,'ch','Switzerland (Swiss Confederation)');
            INSERT INTO `tbl_top_level_domain` VALUES (51,'ci','Ivory Coast (Republic of Côte d''Ivoire)');
            INSERT INTO `tbl_top_level_domain` VALUES (52,'ck','Cook Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (53,'cl','Chile (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (54,'cm','Cameroon (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (55,'cn','China (People''s Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (56,'co','Colombia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (57,'com','Commercial organizations');
            INSERT INTO `tbl_top_level_domain` VALUES (58,'coop','Cooperatives');
            INSERT INTO `tbl_top_level_domain` VALUES (59,'cr','Costa Rica (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (60,'cs','Czechoslovakia {formerly - retired 1995 - see also: .cz and .sk}');
            INSERT INTO `tbl_top_level_domain` VALUES (61,'cu','Cuba (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (62,'cv','Cape Verde (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (63,'cw','Curaçao (Country of)');
            INSERT INTO `tbl_top_level_domain` VALUES (64,'cx','Christmas Island (Territory of)');
            INSERT INTO `tbl_top_level_domain` VALUES (65,'cy','Cyprus (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (66,'cz','Czech Republic');
            INSERT INTO `tbl_top_level_domain` VALUES (67,'dd','German Democratic Republic [East Germany] {formerly - retired}');
            INSERT INTO `tbl_top_level_domain` VALUES (68,'de','Germany (Federal Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (69,'dj','Djibouti (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (70,'dk','Denmark (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (71,'dm','Dominica (Commonwealth of)');
            INSERT INTO `tbl_top_level_domain` VALUES (72,'do','Dominican Republic');
            INSERT INTO `tbl_top_level_domain` VALUES (73,'dz','Algeria (People''s Democratic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (74,'ec','Ecuador (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (75,'edu','Educational establishments');
            INSERT INTO `tbl_top_level_domain` VALUES (76,'ee','Estonia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (77,'eg','Egypt (Arab Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (78,'eh','Western Sahara {reserved}');
            INSERT INTO `tbl_top_level_domain` VALUES (79,'er','Eritrea (State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (80,'es','Spain (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (81,'et','Ethiopia (Federal Democratic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (82,'eu','European Union');
            INSERT INTO `tbl_top_level_domain` VALUES (83,'fi','Finland (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (84,'fj','Fiji (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (85,'fk','Falkland Islands (Malvinas)');
            INSERT INTO `tbl_top_level_domain` VALUES (86,'fm','Micronesia (Federated States of)');
            INSERT INTO `tbl_top_level_domain` VALUES (87,'fo','Faroe Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (88,'fr','France (French Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (89,'ga','Gabon (Gabonese Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (90,'gb','United Kingdom (United Kingdom of Great Britain and Northern Ireland)');
            INSERT INTO `tbl_top_level_domain` VALUES (91,'gd','Grenada');
            INSERT INTO `tbl_top_level_domain` VALUES (92,'ge','Georgia');
            INSERT INTO `tbl_top_level_domain` VALUES (93,'gf','French Guiana');
            INSERT INTO `tbl_top_level_domain` VALUES (94,'gg','Guernsey (Bailiwick of)');
            INSERT INTO `tbl_top_level_domain` VALUES (95,'gh','Ghana (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (96,'gi','Gibraltar');
            INSERT INTO `tbl_top_level_domain` VALUES (97,'gl','Greenland');
            INSERT INTO `tbl_top_level_domain` VALUES (98,'gm','Gambia (Republic of The)');
            INSERT INTO `tbl_top_level_domain` VALUES (99,'gn','Guinea (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (100,'gov','US government');
            INSERT INTO `tbl_top_level_domain` VALUES (101,'gp','Guadeloupe');
            INSERT INTO `tbl_top_level_domain` VALUES (102,'gq','Equatorial Guinea (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (103,'gr','Greece (Hellenic Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (104,'gs','South Georgia and the South Sandwich Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (105,'gt','Guatemala (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (106,'gu','Guam');
            INSERT INTO `tbl_top_level_domain` VALUES (107,'gw','Guinea-Bissau (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (108,'gy','Guyana (Co-operative Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (109,'hk','Hong Kong (Hong Kong Special Administrative Region of the People''s Republic of China)');
            INSERT INTO `tbl_top_level_domain` VALUES (110,'hm','Heard Island and McDonald Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (111,'hn','Honduras (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (112,'hr','Croatia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (113,'ht','Haiti (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (114,'hu','Hungary');
            INSERT INTO `tbl_top_level_domain` VALUES (115,'id','Indonesia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (116,'ie','Ireland (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (117,'il','Israel (State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (118,'im','Isle of Man');
            INSERT INTO `tbl_top_level_domain` VALUES (119,'in','India (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (120,'info','Informational sites');
            INSERT INTO `tbl_top_level_domain` VALUES (121,'int','International treaty-based organizations');
            INSERT INTO `tbl_top_level_domain` VALUES (122,'io','British Indian Ocean Territory');
            INSERT INTO `tbl_top_level_domain` VALUES (123,'iq','Iraq (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (124,'ir','Iran (Islamic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (125,'is','Iceland');
            INSERT INTO `tbl_top_level_domain` VALUES (126,'it','Italy (Italian Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (127,'je','Jersey (Bailiwick of)');
            INSERT INTO `tbl_top_level_domain` VALUES (128,'jm','Jamaica (Commonwealth of)');
            INSERT INTO `tbl_top_level_domain` VALUES (129,'jo','Jordan (Hashemite Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (130,'jobs','Employment-related sites');
            INSERT INTO `tbl_top_level_domain` VALUES (131,'jp','Japan');
            INSERT INTO `tbl_top_level_domain` VALUES (132,'ke','Kenya (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (133,'kg','Kyrgyzstan (Kyrgyz Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (134,'kh','Cambodia (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (135,'ki','Kiribati (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (136,'km','Comoros (Union of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (137,'kn','Saint Kitts and Nevis (Federation of)');
            INSERT INTO `tbl_top_level_domain` VALUES (138,'kp','Korea (Democratic People''s Republic of) [North Korea]');
            INSERT INTO `tbl_top_level_domain` VALUES (139,'kr','Korea (Republic of) [South Korea]');
            INSERT INTO `tbl_top_level_domain` VALUES (140,'kw','Kuwait (State of Kuwait)');
            INSERT INTO `tbl_top_level_domain` VALUES (141,'ky','Cayman Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (142,'kz','Kazakhstan (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (143,'la','Laos (Lao People''s Democratic Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (144,'lb','Lebanon (Lebanese Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (145,'lc','Saint Lucia');
            INSERT INTO `tbl_top_level_domain` VALUES (146,'li','Liechtenstein (Principality of)');
            INSERT INTO `tbl_top_level_domain` VALUES (147,'lk','Sri Lanka (Democratic Socialist Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (148,'local','Pseudo-Domain for Multicast DNS');
            INSERT INTO `tbl_top_level_domain` VALUES (149,'lr','Liberia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (150,'ls','Lesotho (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (151,'lt','Lithuania (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (152,'lu','Luxembourg (Grand Duchy of)');
            INSERT INTO `tbl_top_level_domain` VALUES (153,'lv','Latvia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (154,'ly','Libya');
            INSERT INTO `tbl_top_level_domain` VALUES (155,'ma','Morocco');
            INSERT INTO `tbl_top_level_domain` VALUES (156,'mc','Monaco (Principality of)');
            INSERT INTO `tbl_top_level_domain` VALUES (157,'md','Moldova (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (158,'me','Montenegro');
            INSERT INTO `tbl_top_level_domain` VALUES (159,'mf','Saint Martin (Collectivity of) {unassigned - see also: .gp and .fr}');
            INSERT INTO `tbl_top_level_domain` VALUES (160,'mg','Madagascar (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (161,'mh','Marshall Islands (Republic of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (162,'mil','US military');
            INSERT INTO `tbl_top_level_domain` VALUES (163,'mk','Macedonia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (164,'ml','Mali (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (165,'mm','Myanmar (Republic of the Union of) [Burma]');
            INSERT INTO `tbl_top_level_domain` VALUES (166,'mn','Mongolia');
            INSERT INTO `tbl_top_level_domain` VALUES (167,'mo','Macau (Macau Special Administrative Region of the People''s Republic of China) [Macao]');
            INSERT INTO `tbl_top_level_domain` VALUES (168,'mobi','Mobile');
            INSERT INTO `tbl_top_level_domain` VALUES (169,'mp','Northern Mariana Islands (Commonwealth of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (170,'mq','Martinique');
            INSERT INTO `tbl_top_level_domain` VALUES (171,'mr','Mauritania (Islamic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (172,'ms','Montserrat');
            INSERT INTO `tbl_top_level_domain` VALUES (173,'mt','Malta (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (174,'mu','Mauritius (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (175,'museum','Museums');
            INSERT INTO `tbl_top_level_domain` VALUES (176,'mv','Maldives (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (177,'mw','Malawi (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (178,'mx','Mexico (United Mexican States)');
            INSERT INTO `tbl_top_level_domain` VALUES (179,'my','Malaysia');
            INSERT INTO `tbl_top_level_domain` VALUES (180,'mz','Mozambique (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (181,'na','Namibia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (182,'name','Individuals');
            INSERT INTO `tbl_top_level_domain` VALUES (183,'nato','NATO sites and operations {formerly - retired 1996 - never used}');
            INSERT INTO `tbl_top_level_domain` VALUES (184,'nc','New Caledonia');
            INSERT INTO `tbl_top_level_domain` VALUES (185,'ne','Niger (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (186,'net','Network');
            INSERT INTO `tbl_top_level_domain` VALUES (187,'nf','Norfolk Island (Territory of)');
            INSERT INTO `tbl_top_level_domain` VALUES (188,'ng','Nigeria (Federal Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (189,'ni','Nicaragua (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (190,'nl','Netherlands');
            INSERT INTO `tbl_top_level_domain` VALUES (191,'no','Norway (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (192,'np','Nepal (Federal Democratic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (193,'nr','Nauru (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (194,'nu','Niue');
            INSERT INTO `tbl_top_level_domain` VALUES (195,'nz','New Zealand');
            INSERT INTO `tbl_top_level_domain` VALUES (196,'om','Oman (Sultanate of)');
            INSERT INTO `tbl_top_level_domain` VALUES (197,'onion','Pseudo-Domain for TOR (The Onion Router)');
            INSERT INTO `tbl_top_level_domain` VALUES (198,'org','Non-profit organizations');
            INSERT INTO `tbl_top_level_domain` VALUES (199,'pa','Panama (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (200,'pe','Peru (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (201,'pf','French Polynesia and Clipperton Island');
            INSERT INTO `tbl_top_level_domain` VALUES (202,'pg','Papua New Guinea (Independent State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (203,'ph','Philippines (Republic of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (204,'pk','Pakistan (Islamic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (205,'pl','Poland (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (206,'pm','Saint Pierre and Miquelon');
            INSERT INTO `tbl_top_level_domain` VALUES (207,'pn','Pitcairn Islands (Pitcairn - Henderson - Ducie and Oeno Islands)');
            INSERT INTO `tbl_top_level_domain` VALUES (208,'pr','Puerto Rico (Commonwealth of)');
            INSERT INTO `tbl_top_level_domain` VALUES (209,'pro','Profession');
            INSERT INTO `tbl_top_level_domain` VALUES (210,'ps','Palestine (State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (211,'pt','Portugal (Portuguese Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (212,'pw','Palau (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (213,'py','Paraguay (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (214,'qa','Qatar (State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (215,'re','Réunion');
            INSERT INTO `tbl_top_level_domain` VALUES (216,'ro','Romania');
            INSERT INTO `tbl_top_level_domain` VALUES (217,'rs','Serbia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (218,'ru','Russia (Russian Federation)');
            INSERT INTO `tbl_top_level_domain` VALUES (219,'rw','Rwanda (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (220,'sa','Saudi Arabia (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (221,'sb','Solomon Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (222,'sc','Seychelles (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (223,'sd','Sudan (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (224,'se','Sweden (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (225,'sg','Singapore (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (226,'sh','Saint Helena');
            INSERT INTO `tbl_top_level_domain` VALUES (227,'si','Slovenia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (228,'sj','Svalbard and Jan Mayen {not in use - see also: .no}');
            INSERT INTO `tbl_top_level_domain` VALUES (229,'sk','Slovakia (Slovak Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (230,'sl','Sierra Leone (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (231,'sm','San Marino (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (232,'sn','Senegal (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (233,'so','Somalia (Federal Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (234,'sr','Suriname (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (235,'ss','South Sudan (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (236,'st','São Tomé and Príncipe (Democratic Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (237,'su','Soviet Union (Union of Soviet Socialist Republics)');
            INSERT INTO `tbl_top_level_domain` VALUES (238,'sv','El Salvador (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (239,'sx','Sint Maarten');
            INSERT INTO `tbl_top_level_domain` VALUES (240,'sy','Syria (Syrian Arab Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (241,'sz','Swaziland (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (242,'tc','Turks and Caicos Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (243,'td','Chad (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (244,'tel','Telephone');
            INSERT INTO `tbl_top_level_domain` VALUES (245,'tf','French Southern and Antarctic Lands (Territory of the)');
            INSERT INTO `tbl_top_level_domain` VALUES (246,'tg','Togo (Togolese Republic)');
            INSERT INTO `tbl_top_level_domain` VALUES (247,'th','Thailand (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (248,'tj','Tajikistan (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (249,'tk','Tokelau');
            INSERT INTO `tbl_top_level_domain` VALUES (250,'tl','Timor-Leste (Democratic Republic of) [East Timor]');
            INSERT INTO `tbl_top_level_domain` VALUES (251,'tm','Turkmenistan');
            INSERT INTO `tbl_top_level_domain` VALUES (252,'tn','Tunisia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (253,'to','Tonga (Kingdom of)');
            INSERT INTO `tbl_top_level_domain` VALUES (254,'tp','Timor-Leste (Democratic Republic of) [East Timor] {being phased out - also see: .tl}');
            INSERT INTO `tbl_top_level_domain` VALUES (255,'tr','Turkey (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (256,'travel','Travel');
            INSERT INTO `tbl_top_level_domain` VALUES (257,'tt','Trinidad and Tobago (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (258,'tv','Tuvalu');
            INSERT INTO `tbl_top_level_domain` VALUES (259,'tw','Taiwan (Republic of China)');
            INSERT INTO `tbl_top_level_domain` VALUES (260,'tz','Tanzania (United Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (261,'ua','Ukraine');
            INSERT INTO `tbl_top_level_domain` VALUES (262,'ug','Uganda (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (263,'uk','United Kingdom (United Kingdom of Great Britain and Northern Ireland)');
            INSERT INTO `tbl_top_level_domain` VALUES (264,'um','United States Minor Outlying Islands {formerly - retired 2010 - see also: .us}');
            INSERT INTO `tbl_top_level_domain` VALUES (265,'us','United States of America and United States Minor Outlying Islands');
            INSERT INTO `tbl_top_level_domain` VALUES (266,'uy','Uruguay (Oriental Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (267,'uz','Uzbekistan (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (268,'va','Vatican City (Vatican City State)');
            INSERT INTO `tbl_top_level_domain` VALUES (269,'vc','Saint Vincent and the Grenadines');
            INSERT INTO `tbl_top_level_domain` VALUES (270,'ve','Venezuela (Bolivarian Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (271,'vg','British Virgin Islands (Virgin Islands)');
            INSERT INTO `tbl_top_level_domain` VALUES (272,'vi','United States Virgin Islands (United States Virgin Islands)');
            INSERT INTO `tbl_top_level_domain` VALUES (273,'vn','Vietnam (Socialist Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (274,'vu','Vanuatu (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (275,'wf','Wallis and Futuna (Territory of the Wallis and Futuna Islands)');
            INSERT INTO `tbl_top_level_domain` VALUES (276,'ws','Samoa (Independent State of)');
            INSERT INTO `tbl_top_level_domain` VALUES (277,'xxx','Adult entertainment');
            INSERT INTO `tbl_top_level_domain` VALUES (278,'ye','Yemen (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (279,'yt','Mayotte (Department of)');
            INSERT INTO `tbl_top_level_domain` VALUES (280,'yu','Yugoslavia and Serbia and Montenegro {formerly - retired 2010 - see also: .me and .rs}');
            INSERT INTO `tbl_top_level_domain` VALUES (281,'za','South Africa (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (282,'zm','Zambia (Republic of)');
            INSERT INTO `tbl_top_level_domain` VALUES (283,'zr','Zaire (Republic of) {formerly - retired 2001 - see also: .cd}');
            INSERT INTO `tbl_top_level_domain` VALUES (284,'zw','Zimbabwe (Republic of)');
            CREATE TABLE IF NOT EXISTS `tbl_ping` (
	            `Ping_ID`	INTEGER PRIMARY KEY AUTOINCREMENT,
	            `TTL`	INTEGER,
	            `Bytes`	INTEGER,
	            `IP`	TEXT NOT NULL,
	            `Status`	TEXT,
	            `ping_time`	INTEGER,
	            `timestamp`	TEXT
            );
            CREATE TABLE IF NOT EXISTS `tbl_config` (
	            `Config_ID`	INTEGER PRIMARY KEY AUTOINCREMENT,
	            `IP`	TEXT NOT NULL,
	            `Interval`	INTEGER NOT NULL,
	            `Range`	INTEGER NOT NULL,
	            `TimeError`	INTEGER NOT NULL,
	            `autostart`	INTEGER NOT NULL DEFAULT 0
            );
            COMMIT;";
            ExecuteQuery(sql);
            
            
        }
 

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addPing = new AddPing();
            addPing.Activate();
            addPing.Visibility = Visibility.Visible;
            addPing.Owner = this;

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (PingBox child in pingStackpanel.Children)
            { 
                if (child.isChecked())
                {
                    child.startPing();
                    child.checkboxUnchecked();
                    checkall.IsChecked = false;
                }
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
 
            PingBox[] toDelete = new PingBox[pingStackpanel.Children.Count];

            int count=0;
            string sql;

            foreach (PingBox child in pingStackpanel.Children)
            {

                if (child.isChecked())
                {
                    child.stopPing();
                    sql = String.Format("delete  from tbl_config where Config_ID = {0};", child.getConfigId()); 
                    ExecuteQuery(sql);
                    checkall.IsChecked = false;
                    toDelete[count] = child;
                    count++;
                }
                
            }
            for(int i = 0; i < toDelete.Length; i++)
            {
                
                if ((toDelete[i]) == null) {
                    break;
                }
                else
                { 
                    pingStackpanel.Children.Remove(toDelete[i]);
                }
               
              
            }
            
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (PingBox child in pingStackpanel.Children)
            {

                if (child.isChecked())
                {
                    child.stopPing();
                    child.checkboxUnchecked();
                    checkall.IsChecked = false;//Lokal in der Navigationsbar
                }
            }

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkall.IsChecked.HasValue && checkall.IsChecked.Value)
            {
                foreach(PingBox child in pingStackpanel.Children)
                {
                    child.checkboxChecked();
                }
            }
            else
            {
                foreach(PingBox child in pingStackpanel.Children)
                {
                    child.checkboxUnchecked();
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
   
        public void addPanel(PingBox pingbox)
        {
            pingStackpanel.Children.Add(pingbox);
        }
    }
}

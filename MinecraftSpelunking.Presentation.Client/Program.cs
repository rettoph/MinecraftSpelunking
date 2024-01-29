using MinecraftPinger.Library;

JavaPinger java = new JavaPinger();

var response = java.Ping("127.0.0.1", 25565);

Console.ReadLine();
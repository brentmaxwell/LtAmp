namespace LtAmpDotNet.Cli.MidiCommands
{
    public enum StompEffect
    {
        DUBS_Passthru = 0,
        DUBS_Overdrive = 1,  //OVERDRIVE
        DUBS_VariFuzz = 4,  //FUZZ
        DUBS_SimpleCompressor = 6,  //COMPRESSOR
        DUBS_Greenbox = 9,  //BLUES DRIVE
        DUBS_Blackbox = 11, //ROCK DIRT
        DUBS_BigFuzz = 12, //BIG FUZZ
        DUBS_ChromeGate = 13, //METAL GATE
        DUBS_MustangFiveBandEq1 = 14, //5-BAND EQ
        DUBS_MythicDrive = 15, //MYTHIC DRIVE
        DUBS_Octobot = 16, //OCTOBOT
        DUBS_Sustain = 17, //Sustain
    }

    public enum ModEffect
    {
        DUBS_Passthru = 0,
        DUBS_ChorusTriangle = 2,  //CHORUS
        DUBS_TriangleFlanger = 4,  //FLANGER
        DUBS_Vibratone = 5,  //VIBRATONE
        DUBS_SineTremolo = 7,  //TREMOLO
        DUBS_StepFilter = 9,  //STEP FILTER
        DUBS_Phaser = 10, //PHASER
        DUBS_EcFilter = 13, //TOUCH WAH
    }

    public enum DelayEffect
    {
        DUBS_Passthru = 0,
        DUBS_MonoDelay = 1,  //DELAY
        DUBS_ReverseDelay = 7,  //REVERSE
        DUBS_TapeDelayLite = 8,  //ECHO
    }

    public enum ReverbEffect
    {
        DUBS_Passthru = 0,
        DUBS_LargeHallReverb = 2,  //LARGE HALL
        DUBS_SmallRoomReverb = 3,  //SMALL ROOM
        DUBS_LargePlate = 6,  //PLATE
        DUBS_ArenaReverb = 8,  //ARENA
        DUBS_Spring65 = 10, //SPRING 65
    }

    public enum Amplifiers
    {
        DUBS_Deluxe57 = 1,  //DELUXE DIRT
        DUBS_Bassman59 = 2,  //BASSMAN
        DUBS_Champ57 = 3,  //CHAMPION
        DUBS_Deluxe65 = 4,  //DELUXE CLN
        DUBS_Princeton65 = 5,  //PRINCETON
        DUBS_Twin65 = 6,  //TWIN CLEAN
        DUBS_SuperSonic = 7,  //BURN 
        DUBS_Ac30Tb = 8,  //60S UK CLEAN
        DUBS_DR103 = 9,  //70S UK CLEAN
        DUBS_Jcm800 = 10, //80S ROCK
        DUBS_Rect2 = 11, //90S ROCK
        DUBS_Evh3 = 12, //METAL 2000
        DUBS_LinearGain = 13, //SUPER CLEAN
        DUBS_Twin57 = 14, //50S TWIN
        DUBS_Excelsior = 18, //EXCELSIOR
        DUBS_MetalEvh3 = 19, //SUPER HEAVY
        DUBS_MetalRect2 = 20, //ALT METAL
        DUBS_Or120 = 21, //DOOM METAL
        DUBS_Plexi87 = 22, //70S ROCK
        DUBS_Silvertone = 23, //SMALLTONE
    }
}

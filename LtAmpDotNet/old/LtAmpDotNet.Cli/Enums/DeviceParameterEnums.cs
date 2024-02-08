namespace LtAmpDotNet.Cli.Enums
{
    public enum StompEffect
    {
        DUBS_Passthru = 0,
        DUBS_Overdrive = 1,  //OVERDRIVE
        DUBS_VariFuzz = 2,  //FUZZ
        DUBS_SimpleCompressor = 3,  //COMPRESSOR
        DUBS_Greenbox = 4,  //BLUES DRIVE
        DUBS_Blackbox = 5, //ROCK DIRT
        DUBS_BigFuzz = 6, //BIG FUZZ
        DUBS_ChromeGate = 7, //METAL GATE
        DUBS_MustangFiveBandEq1 = 8, //5-BAND EQ
        DUBS_MythicDrive = 9, //MYTHIC DRIVE
        DUBS_Octobot = 10, //OCTOBOT
        DUBS_Sustain = 11, //Sustain
    }

    public enum ModEffect
    {
        DUBS_Passthru = 0,
        DUBS_ChorusTriangle = 1,  //CHORUS
        DUBS_TriangleFlanger = 2,  //FLANGER
        DUBS_Vibratone = 3,  //VIBRATONE
        DUBS_SineTremolo = 4,  //TREMOLO
        DUBS_StepFilter = 5,  //STEP FILTER
        DUBS_Phaser = 6, //PHASER
        DUBS_EcFilter = 7, //TOUCH WAH
    }

    public enum DelayEffect
    {
        DUBS_Passthru = 0,
        DUBS_MonoDelay = 1,  //DELAY
        DUBS_ReverseDelay = 2,  //REVERSE
        DUBS_TapeDelayLite = 3,  //ECHO
    }

    public enum ReverbEffect
    {
        DUBS_Passthru = 0,
        DUBS_LargeHallReverb = 1,  //LARGE HALL
        DUBS_SmallRoomReverb = 2,  //SMALL ROOM
        DUBS_LargePlate = 3,  //PLATE
        DUBS_ArenaReverb = 4,  //ARENA
        DUBS_Spring65 = 5, //SPRING 65
    }

    public enum Amplifiers
    {
        DUBS_LinearGain = 0, //SUPER CLEAN
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
        DUBS_Twin57 = 13, //50S TWIN
        DUBS_Excelsior = 14, //EXCELSIOR
        DUBS_MetalEvh3 = 15, //SUPER HEAVY
        DUBS_MetalRect2 = 16, //ALT METAL
        DUBS_Or120 = 17, //DOOM METAL
        DUBS_Plexi87 = 18, //70S ROCK
        DUBS_Silvertone = 19, //SMALLTONE
    }
}

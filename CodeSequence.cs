using System.Media;
using Timer = System.Timers.Timer;

namespace SuperMarioArturoBros
{
    public class CodeSequence
    {
        readonly Keys[] _code = { Keys.D, Keys.I, Keys.E};

        private int _sequenceIndex;

        private readonly int _codeLength;
        private readonly int _sequenceMax;

        private readonly Timer _quantum = new Timer();

        public SoundPlayer sound;

        public CodeSequence()
        {
            _codeLength = _code.Length - 1;
            _sequenceMax = _code.Length;

            _quantum.Interval = 5000; //ms before reset
            _quantum.Elapsed += timeout;
        }

        public bool IsCompletedBy(Keys key)
        {
            _quantum.Start();

            _sequenceIndex %= _sequenceMax;
            _sequenceIndex = (_code[_sequenceIndex] == key) ? ++_sequenceIndex : 0;

            return _sequenceIndex > _codeLength;
        }

        private void timeout(object o, EventArgs e)
        {
            _quantum.Stop();
            _sequenceIndex = 0;
        }

        public void EasterEgg()
        {

            sound = new SoundPlayer(Resource1.japan);
            sound.Play();
        }
    }
}

using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Containers.CompoundKeys
{
    public class VectorAssignmentEntry
    {
        public string VectorSetName { get; }
        public StudentEnrollmentState StartState { get; }
        public StudentEnrollmentState EndState { get; }

        public VectorAssignmentEntry(
            string vectorSetName, 
            StudentEnrollmentState startState, 
            StudentEnrollmentState endState)
        {
            VectorSetName = vectorSetName;
            StartState = startState;
            EndState = endState;
        }

        public static bool operator ==(
            VectorAssignmentEntry vectorAssignmentEntryOne, 
            VectorAssignmentEntry vectorAssignmentEntryTwo)
        {
            if (ReferenceEquals(vectorAssignmentEntryOne, vectorAssignmentEntryTwo))
            {
                return true;
            }

            if (ReferenceEquals(vectorAssignmentEntryOne, null))
            {
                return false;
            }

            return vectorAssignmentEntryOne.Equals(vectorAssignmentEntryTwo);
        }

        public static bool operator !=(
            VectorAssignmentEntry vectorAssignmentEntryOne, 
            VectorAssignmentEntry vectorAssignmentEntryTwo)
        {
            return !(vectorAssignmentEntryOne == vectorAssignmentEntryTwo);
        }

        public override bool Equals(object obj)
        {
            // Note, if the obj was null, this safe caste would just return null
            var vectorAssignmentEntry = obj as VectorAssignmentEntry;
            if (vectorAssignmentEntry == null) return false;

            var isEqual = vectorAssignmentEntry.VectorSetName == VectorSetName
                       && vectorAssignmentEntry.StartState == StartState
                       && vectorAssignmentEntry.EndState == EndState;

            return isEqual;
        }

        /// <summary>
        /// Note, this implemenatation was informed by the following thread:
        /// http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int primeNumberOne = 13;
                int primeNumberTwo = 5;

                var hash = primeNumberOne;
                hash = (hash * primeNumberTwo) + VectorSetName.GetHashCode();
                hash = (hash * primeNumberTwo) + StartState.GetHashCode();
                hash = (hash * primeNumberTwo) + EndState.GetHashCode();

                return hash;
            }
        }
    }
}
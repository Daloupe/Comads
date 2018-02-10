using System;
using System.Collections.Generic;
using System.Linq;
using Core.SharedKernel;
using Core.ValueObjects;


namespace Core.Entities
{
    public class Person : BaseEntity
    {
        public Person(string headAncestorId, (Name first, Names middle, Name last) names, DateTime DOB)
        {
            CompassPersonId = Guid.NewGuid();
            Id = Guid.NewGuid().ToString();

            HeadAncestorId = headAncestorId;

            UtcCreatedTimestamp = DateTime.UtcNow;

            FirstName = names.first;
            MiddleNames = names.middle;
            LastName = names.last;

            DateOfBirth = DOB.ToUniversalTime().Date;
        }


        public Guid CompassPersonId { get;  }

        public string HeadAncestorId { get;  }


        public DateTime UtcCreatedTimestamp { get;  }

        public Guid ApiKey { get;  }


        public string Username { get;  }

        public string Password { get;  }

        public string TemporaryPassword { get;  }
        public string PasswordResetHash { get;  }

        public string UserHash { get;  }
        
        public Name FirstName { get; }
        public Names MiddleNames { get; }
        public Name LastName { get; }

        public DateTime DateOfBirth { get;  }

        public string BirthCountry { get;  }
        public string Nationality { get;  }

        public bool InterpreterRequired { get;  }


        public string Interests { get;  }
        public int? VisaSubclass { get;  }


        public string FingerprintData { get;  }


        public BoolValueObject CompassEmailAllowed { get;  }

        public BoolValueObject CompassSmsAllowed { get;  }

        public string ContactNotesBh { get;  }
        public string ContactNotesAh { get;  }

        public BoolValueObject CanContactAtWork { get;  }

        public string Religion { get;  }

        public string ReligionNotes { get;  }
        public string ReligiousOrder { get;  }

        public string Parish { get;  }


        public BoolValueObject AmbulanceSubscriber { get;  }

        public BoolValueObject MedicalAlert { get;  }

        public string Occupation { get;  }
        public string Employer { get;  }

        public BoolValueObject Disability { get;  }

        public BoolValueObject DisabilityFunded { get;  }

        public BoolValueObject HearingImpairment { get;  }

        public BoolValueObject PhysicalImpairment { get;  }

        public BoolValueObject MobilityImpairment { get;  }

        public BoolValueObject SpeechImpairment { get;  }

        public BoolValueObject VisualImpairment { get;  }

        public string GovtCode1 { get;  }
        public string GovtCode2 { get;  }

        public string ActivityRestrictionNotes { get;  }
        public string AtRiskNotes { get;  }

        public bool YouthAllowance { get;  }

        public bool PublishPhotoToMedia { get;  }

        public bool PublishPhotoToInternal { get;  }

        public bool ProofImmunisation { get;  }

        public bool? HeadLiceCheck { get;  }

        public bool RequiresEslAssistance { get;  }

        public bool ReceivesEslAssistance { get;  }

        public bool LanguageBackgroundOtherThanEnglish { get;  }

        public bool PasswordResetRequired { get;  }
}
}
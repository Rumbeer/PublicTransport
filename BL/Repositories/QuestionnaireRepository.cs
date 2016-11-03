﻿using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class QuestionnaireRepository : EntityFrameworkRepository<Questionnaire, int>
    {
        public QuestionnaireRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}

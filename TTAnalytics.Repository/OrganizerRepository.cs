﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TTAnalytics.Data;
using TTAnalytics.Model;
using TTAnalytics.RepositoryInterface;

namespace TTAnalytics.Repository
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private TTAnalyticsContext context;

        public OrganizerRepository(TTAnalyticsContext context)
        {
            this.context = context;
        }

        public ICollection<Organizer> GetAll()
        {
            return context.Organizers
                .ToList();
        }

        public Organizer Get(int id)
        {
            return context.Organizers
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }


        public Organizer Add(Organizer organizer)
        {
            var result = context.Organizers.Add(organizer);

            Save();

            return result;
        }

        public Organizer Update(Organizer organizer)
        {
            context.Entry(organizer).State = EntityState.Modified;

            Save();

            return organizer;
        }


        public void Delete(int id)
        {
            Organizer organizer = context.Organizers.Find(id);
            context.Organizers.Remove(organizer);

            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

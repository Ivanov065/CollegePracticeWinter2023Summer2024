using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Center_Client_App
{
	enum ServerRequests
	{
		EndConnection,
		LoginUser,
		LoadClient,
		LoadTickets,
		LoadSchedule,
		LoadEmployee,
		LoadTrainerSchedule,
		LoadTrainers,
		LoadClients,
		LoadActiveTicket,
		LoadSingleTicket,
		LoadManageTicketsInfo,
		AddTicket,
		UpdateTicket,
		DeleteTicket,
		LoadManageScheduleInfo,
		LoadSingleSchedule,
		AddSchedule,
		UpdateSchedule,
		DeleteSchedule,
		LoadRoles,
		LoadOnlyClient,
		LoadUser,
		UpdateClient,
		AddClient,
		DeleteClient,
		UpdateUser,
		AddUser,
		DeleteUser
	}

	enum Roles
	{
		Client = 1,
		Manager,
		Administrator,
		Trainer
	}

	public class User
	{
		int id;
		string role;

		public int ID { get { return id; } set { id = value; } }
		public string Role { get { return role; } set { role = value; } }
		public User(int id_, string role_)
		{
			id = id_;
			role = role_;
		}
	}

	public class Client
	{
		string name;
		string surname;
		string middleName;
		string adres;
		string phone;
		DateOnly date;

		public string Name { get { return name; } set { name = value; } }
		public string Surname { get { return surname; } set { surname = value; } }
		public string Middlename { get { return middleName; } set { middleName = value; } }
		public string Adres { get { return adres; } set { adres = value; } }
		public string Phone { get { return phone; } set { phone = value; } }
		public DateOnly Date { get { return date; } set { date = value; } }
		public Client(string name, string surname, string middleName,
			string adres, string phone, DateOnly date)
		{
			this.name = name;
			this.surname = surname;
			this.middleName = middleName;
			this.adres = adres;
			this.phone = phone;
			this.date = date;
		}
	}

	public class SeasonTicket
	{
		int ticket_id;
		int days_long;
		string training_name;
		string service_name;
		double price;
		int visiting_amount;

		public int TicketId { get { return ticket_id; } set { ticket_id = value; } }
		public int DaysLong { get { return days_long; } set { days_long = value; } }
		public string TrainingName { get { return training_name; } set { training_name = value; } }
		public string ServiceName { get { return service_name; } set { service_name = value; } }
		public double Price { get { return price; } set { price = value; } }
		public int VisitingAmount { get { return visiting_amount; } set { visiting_amount = value; } }

		public SeasonTicket(int ticket_id, int days_long,
			string training_name, string service_name, double price, int visiting_amount)
		{
			this.ticket_id = ticket_id;
			this.days_long = days_long;
			this.training_name = training_name;
			this.service_name = service_name;
			this.price = price;
			this.visiting_amount = visiting_amount;
		}
	}

	public class ClientSeasonTicket
	{
		int ticket_id;
		DateOnly date_start;
		DateOnly date_end;
		int last_visits;
		string training_name;
		string service_name;
		double price;
		int visiting_amount;

		public int TicketId { get { return ticket_id; } set { ticket_id = value; } }
		public DateOnly DateStart { get { return date_start; } set { date_start = value; } }
		public DateOnly DateEnd { get { return date_end; } set { date_end = value; } }
		public int LastVisits { get { return last_visits; } set { last_visits = value; } }
		public string TrainingName { get { return training_name; } set { training_name = value; } }
		public string ServiceName { get { return service_name; } set { service_name = value; } }
		public double Price { get { return price; } set { price = value; } }
		public int VisitingAmount { get { return visiting_amount; } set { visiting_amount = value; } }

		public ClientSeasonTicket(int ticket_id, DateOnly date_start, DateOnly date_end, int last_visits,
			string training_name, string service_name, double price, int visiting_amount)
		{
			this.ticket_id = ticket_id;
			this.date_start = date_start;
			this.date_end = date_end;
			this.last_visits = last_visits;
			this.training_name = training_name;
			this.service_name = service_name;
			this.price = price;
			this.visiting_amount = visiting_amount;
		}
	}
}

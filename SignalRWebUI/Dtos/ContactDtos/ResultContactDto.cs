﻿namespace SignalRWebUI.Dtos.ContactDtos
{
	public class ResultContactDto
    {
        public int ContactID { get; set; }
		public string Location { get; set; }
		public string PhoneNumber { get; set; }
		public string Mail { get; set; }
		public string FooterTitle { get; set; }
		public string FooterDescription { get; set; }
		public string OpenDays { get; set; }
		public string OpenDaysDescription { get; set; }
		public string OpeningHours { get; set; }
	}
}

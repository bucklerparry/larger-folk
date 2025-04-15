using System;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	[Serializable]
	public class LargerFolk_Obese_old : BaseMutation
	{

		public override void SaveData(SerializationWriter Writer)
		{
			base.SaveData(Writer);
		}

		public override void LoadData(SerializationReader Reader)
		{
			base.LoadData(Reader);
		}

		public override void FinalizeLoad()
		{
			/*
			foreach (int id in RegisteredPartIDs)
			{
				UnityEngine.Debug.Log("BodyPart id: " + id + ", name: " + ParentObject?.Body?._Body?.GetPartByID(id)?.GetOrdinalName());
			}
			*/
			base.FinalizeLoad();
		}

		public LargerFolk_Obese_old()
		{

			DisplayName = "Incredibly overweight";
		}

		public override bool UseVariantName => false; // Does not currently respect defect status

		public override bool CanLevel()
		{
			return false;
		}

	

		
	}
}

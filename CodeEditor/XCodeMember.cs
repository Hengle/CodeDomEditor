//
//  XCodeBase.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System.Runtime.Serialization;

namespace wuxingogo.Code
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System.Reflection;
	using System.CodeDom;
	using System;
	using Object = UnityEngine.Object;

//	[System.Serializable]
	public class XCodeMember
	{
		public string name = "Default";

		public virtual void DrawSelf(XBaseWindow window){
		}

		public List<string> comments = new List<string>();
		public List<XCodeCustomAttribute> attributes = new List<XCodeCustomAttribute>();

		public XCodeType type;
		public MemberAttributes memberAttribute = MemberAttributes.Public;
		public bool isSerialize = false;

		public XCodeMember()
		{
		}
		[OnSerializing]
		void OnSerialize()
		{
			if (!isSerialize) {
				isSerialize = true;
				type = XCodeTypeTemplate.GetInstance().GetTemplate(typeof(void));

			}
		}

		public virtual void DrawType(XBaseWindow window)
		{
			XBaseWindow.DoButton("Type", ()=> {
				XCodeTypeTemplate.SelectType(x => type = x);
			});

			memberAttribute = (MemberAttributes)XBaseWindow.CreateEnumPopup( memberAttribute );

		
		}
//		public virtual void DrawMemberAttribute(XBaseWindow window)
//		{
//			memberAttribute = window.CreateEnumSelectable( memberAttribute ) as MemberAttributes;
//		}

		public virtual void DrawComments(XBaseWindow window)
		{
			XBaseWindow.CreateLabel( "Comments" );
			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				XBaseWindow.BeginHorizontal();
				comments[pos] = XBaseWindow.CreateStringField( comments[pos] );
				XBaseWindow.DoButton( "Delete", () => comments.RemoveAt( pos ) );
				XBaseWindow.EndHorizontal();
			}
		}

		public virtual void DrawCustomeAttribute(XBaseWindow window)
		{
			XBaseWindow.CreateLabel( "CustomAttributes" );
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in comments.Count
				XBaseWindow.BeginHorizontal();
				attributes[pos].name = XBaseWindow.CreateStringField( attributes[pos].name );
				XBaseWindow.DoButton<XCodeParameter>( "Parameter", ParameterCreate, attributes[pos].parameter);
				XBaseWindow.DoButton( "Delete", () => attributes.RemoveAt( pos ) );
				XBaseWindow.EndHorizontal();
			}
		}

		public void ParameterCreate(XCodeParameter parameter){
			XParameterEditor.Init<XParameterEditor>(parameter);
		}
   
	}
}


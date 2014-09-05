<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manager.aspx.cs" Inherits="CurrencyService.manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Curencies</title>
</head>
<body>

    <h1>Currencies</h1>

    <form id="form1" runat="server">
    <div>
        <asp:ListView ID="CurrencyListView" runat="server"  onitemcommand="CurrencyListView_ItemCommand"  OnItemUpdating="CurrencyListView_ItemUpdating" OnItemEditing="CurrencyListView_ItemEditing" OnItemCanceling="CurrencyListView_ItemCanceling">

            <LayoutTemplate>
             <table border="0" cellpadding="1">
              <tr style="background-color:#E5E5FE">
               <th align="left">Id</th>
               <th align="left">Code</th>
               <th align="left">Value</th>
               <th></th>
              </tr>
              <tr id="itemPlaceholder" runat="server"></tr>
             </table>
            </LayoutTemplate>
            <ItemTemplate>
              <tr>
               <td><asp:Label runat="server" ID="lblId"><%#Eval("ID") %></asp:Label></td>
               <td><asp:Label runat="server" ID="lblCode"><%#Eval("Code") %></asp:Label></td>
               <td><asp:Label runat="server" ID="lblValue"><%#Eval("Value") %></asp:Label></td>
                        <td><asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton></td>
               <td></td>
              </tr>
            </ItemTemplate>
            <EditItemTemplate>
                    <td>
                        <asp:TextBox ID="txtUpId" runat="server" Text='<%#Eval("Id") %>' Enabled="false" Width="20px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUpCode" runat="server" Text='<%#Eval("Code") %>'  Enabled="false" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUpValue" runat="server" Width="100px" Text='<%#Eval("Value") %>'></asp:TextBox>
                     </td>
                     <td>   
                            <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update">Update</asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                    </td>
                    
                </tr>
            </EditItemTemplate>
        </asp:ListView>


    
    </div>
    </form>
</body>
</html>

﻿using System;
using System.Collections.Generic;
using UCCollaborationLib;
using System.Runtime.InteropServices;
using Websocket.Client;
using System.Text.Json.Nodes;

namespace MMPresenceProviderImpl
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class IMClientContactSubscription : ContactSubscription
    {
        public IMClientContactSubscription()
        {
            _subscribedContacts = new Dictionary<string, IMClientContact>();
            _store = PresenceProvider.client.Store;
        }

        // Store references to all of the IContact objects to subscribe to.
        private Dictionary<string, IMClientContact> _subscribedContacts;
        private Mattermost.Store _store;

        // Add a new IContact object to the collection of contacts.
        public void AddContact(Contact _contact)
        {
            if (!_subscribedContacts.ContainsKey(_contact.Uri))
            {
                _subscribedContacts.Add(_contact.Uri, _contact as IMClientContact);
            }
        }

        public void Subscribe(ContactSubscriptionRefreshRate _subscriptionFreshness, ContactInformationType[] _contactInformationTypes)
        {
            PresenceProvider.client.WsClient.MessageReceived.Subscribe(handleWebsocketEvent);
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }

        public void RemoveContact(Contact _contact)
        {
            if( _subscribedContacts.ContainsKey(_contact.Uri))
            {
                _subscribedContacts.Remove(_contact.Uri);
            }
        }

        private void handleWebsocketEvent(ResponseMessage msg)
        {
            string email = JsonNode.Parse(msg.ToString())[Mattermost.Constants.MattermostEmail].GetValue<string>();
            string status = JsonNode.Parse(msg.ToString())[Mattermost.Constants.MattermostStatus].GetValue<string>();
            _store.Add(email, status);
            IMClientContact contact;
            if (_subscribedContacts.TryGetValue(email, out contact))
            {
                ContactInformationChangedEventData eventData = new IMClientContactInformationChangedEventData();
                contact.RaiseOnContactInformationChangedEvent(eventData);
            }
        }

        public void AddContactByUri(string _contactUri)
        {
            throw new NotImplementedException();
        }

        public void AddContacts(Contact[] _contacts)
        {
            throw new NotImplementedException();
        }

        public ContactSubscriptionRefreshRate LastSubscribedRefreshRate => throw new NotImplementedException();

        public ContactInformationType[] LastSubscribedContactInformation => throw new NotImplementedException();

        public Contact[] Contacts => throw new NotImplementedException();
    }
}

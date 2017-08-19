import { Meteor } from 'meteor/meteor';
import { ServiceConfiguration } from 'meteor/service-configuration';

ServiceConfiguration.configurations.remove({
  service: "twitch"
});
ServiceConfiguration.configurations.insert({
  service: "twitch",
  clientId: Meteor.settings.twitch.clientId,
  redirectUri: Meteor.absoluteUrl() + '_oauth/twitch?close',
  secret: Meteor.settings.twitch.secret
});
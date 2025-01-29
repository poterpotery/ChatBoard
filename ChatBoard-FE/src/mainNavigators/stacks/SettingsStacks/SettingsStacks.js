import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Settings from '../../../screens/Settings/Settings';
import Profile from '../../../screens/Profile/Profile';
import DeleteAccount from '../../../screens/DeleteAccount/DeleteAccount';
import UpdateProfle from '../../../screens/UpdateProfle/UpdateProfle';



const Stack = createNativeStackNavigator();
// @refresh reset
const SettingsStacks = ({ route }) => {
  return (
    <Stack.Navigator
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name="Settings" component={Settings} />
      <Stack.Screen name="Profile" component={Profile} />
      <Stack.Screen name="DeleteAccount" component={DeleteAccount} />
      <Stack.Screen name="UpdateProfile" component={UpdateProfle} />
    </Stack.Navigator>
  );
};
export default SettingsStacks;

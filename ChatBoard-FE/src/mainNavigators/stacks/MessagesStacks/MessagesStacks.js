import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Messages from '../../../screens/Messages/Messages';
import PersonalMessage from '../../../screens/PersonalMessage/PersonalMessage';



const Stack = createNativeStackNavigator();
// @refresh reset
const MessagesStacks = ({ route }) => {
  return (
    <Stack.Navigator
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name="Messages" component={Messages} />
      <Stack.Screen name="PersonalMessage" component={PersonalMessage} />
    </Stack.Navigator>
  );
};
export default MessagesStacks;

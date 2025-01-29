import AsyncStorage from '@react-native-async-storage/async-storage';
import { GoogleSignin } from '@react-native-google-signin/google-signin';
import { setToken } from '../store/authentication';
import { setMessages } from '../store/chat';
import { setContacts } from '../store/contacts';
import { setRoster } from '../store/roster';
import { setUnreadMessages } from '../store/chatState';
import { setOnlineStatus } from '../store/onlineStatus';
import { CommonActions, useNavigation } from '@react-navigation/native';
import { api } from '../services/api';
import { setGlobalCount, setMessagesCount } from '../store/messagesCount';
import { URL } from '../Constants';
import ServerApi from '../AxiosApi';
import { apiTags } from '../services/apiTags';
import { resetProgressBar } from '../store/progressBar';
import { setMute } from '../store/video';
import { disconnect } from '../store/socket';
import FastImage from 'react-native-fast-image';
import { resetBoostedPost } from '../store/boostedPost';
import { resetTime } from '../store/timeSpent';

const resetAction = CommonActions.reset({
  index: 0, // Replace the stack with only one screen
  routes: [{ name: 'Authentication' }], // Navigate to 'Authentication'
});

export const signOut = async () => {
  try {
    const result = await GoogleSignin.signOut();
  } catch (error) {
  }
};

export const LougoutHandler = (dispatch, navigation) => {
  ServerApi.get(URL + '/Auth/LogOut')
    .then(res => {
      dispatch(api.util.resetApiState());
      dispatch(apiTags.util.resetApiState());
      dispatch(setToken(''));
      dispatch(resetProgressBar());
      dispatch(setMessages('disconnect'));
      dispatch(setUnreadMessages('disconnect'));
      dispatch(disconnect());
      dispatch(setMute('disconnect'));
      dispatch(setContacts('disconnect'));
      dispatch(setRoster('disconnect'));
      dispatch(setOnlineStatus('disconnect'));
      dispatch(setGlobalCount('disconnect'));
      dispatch(setMessagesCount('disconnect'));
      dispatch(resetBoostedPost());
      dispatch(resetTime());
      AsyncStorage.removeItem('token');
      AsyncStorage.removeItem('accountid');
      AsyncStorage.removeItem('countryCode');
      AsyncStorage.removeItem('country');
      // AsyncStorage.removeItem('boostedPost');
      
      // FastImage.clearMemoryCache();
      // FastImage.clearDiskCache();
      // api.util.resetApiState();
      signOut();
    })
    .catch(err => {
    });

  navigation.dispatch(resetAction);
};

export const LougoutLocalHandler = (dispatch, navigation) => {
  dispatch(api.util.resetApiState());
  dispatch(apiTags.util.resetApiState());
  dispatch(setToken(''));
  dispatch(resetProgressBar());
  dispatch(setMute('disconnect'));
  dispatch(setMessages('disconnect'));
  dispatch(setContacts('disconnect'));
  dispatch(setRoster('disconnect'));
  dispatch(setOnlineStatus('disconnect'));
  dispatch(setMessagesCount('disconnect'));
  AsyncStorage.removeItem('token');
  AsyncStorage.removeItem('accountid');
  // api.util.resetApiState();
  // signOut()
  // navigation.dispatch(resetAction);
};

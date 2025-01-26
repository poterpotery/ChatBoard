import 'react-native-gesture-handler';
import React, { useEffect, useRef, useState } from 'react';
import { Provider, useDispatch } from 'react-redux';
import { PersistGate } from 'redux-persist/lib/integration/react';
import { store, persistor } from './src/store';
// import ApplicationNavigator from './navigators/Application';
import {
  LogBox,
} from 'react-native';
import { ToastProvider, useToast } from 'react-native-toast-notifications';
import { MenuProvider } from 'react-native-popup-menu';

// import NotificationToast from './components/Toasts/NotificationToast';
// import 'react-native-get-random-values';
import ApplicationNavigator from './src/mainNavigators/ApplicationNavigator';
import CustomToast from './src/components/CustomToast/CustomToast';
const AppWrapper = () => {

  LogBox.ignoreAllLogs(); //Ignore all log notifications

  return (
      <Provider store={store}>
        <MenuProvider>
          <ToastProvider
            swipeEnabled={true}
            offsetBottom={40}
            renderType={{
              success: toast => <CustomToast toast={toast} type={'Success'} />,
              warning: toast => <CustomToast toast={toast} type={'Warning'} />,
              error: toast => <CustomToast toast={toast} type={'Error'} />,
              danger: toast => <CustomToast toast={toast} type={'Error'} />,
            }}
          >
            {/**
             * PersistGate delays the rendering of the app's UI until the persisted state has been retrieved
             * and saved to redux.
             * The `loading` prop can be `null` or any react instance to show during loading (e.g. a splash screen),
             * for example `loading={<SplashScreen />}`.
             * @see https://github.com/rt2zz/redux-persist/blob/master/docs/PersistGate.md
             */}
            <PersistGate loading={null} persistor={persistor}>
                <ApplicationNavigator />
            </PersistGate>
          </ToastProvider>
        </MenuProvider>
      </Provider>
  );
};

export default AppWrapper;

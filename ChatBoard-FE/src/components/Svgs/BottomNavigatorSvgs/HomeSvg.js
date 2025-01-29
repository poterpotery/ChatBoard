import React from 'react';
import { View } from 'react-native';
import Svg, { Defs, LinearGradient, Path, Stop } from 'react-native-svg';

const HomeSvg = ({ isFocused, width = 29, height = 29 }) => {
    return (
        <>


            <Svg width="21" height="18" viewBox="0 0 21 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                <Path
                    d="M7.875 1.69516H21V2.94937H7.875V1.69516ZM7.875 9.22047V7.96625H21V9.22047H7.875ZM7.875 15.4916V14.2373H21V15.4916H7.875ZM2.625 6.71203C2.9873 6.71203 3.32568 6.77735 3.64014 6.908C3.95459 7.03865 4.23486 7.21829 4.48096 7.44692C4.72705 7.67556 4.91504 7.94012 5.04492 8.24061C5.1748 8.5411 5.24316 8.86772 5.25 9.22047C5.25 9.56668 5.18164 9.89004 5.04492 10.1905C4.9082 10.491 4.72021 10.7588 4.48096 10.994C4.2417 11.2292 3.96484 11.4088 3.65039 11.5329C3.33594 11.657 2.99414 11.7224 2.625 11.7289C2.2627 11.7289 1.92432 11.6636 1.60986 11.5329C1.29541 11.4023 1.01514 11.2226 0.769043 10.994C0.522949 10.7654 0.334961 10.5008 0.205078 10.2003C0.0751953 9.89984 0.00683594 9.57322 0 9.22047C0 8.87425 0.0683594 8.5509 0.205078 8.25041C0.341797 7.94992 0.529785 7.68209 0.769043 7.44692C1.0083 7.21176 1.28516 7.03212 1.59961 6.908C1.91406 6.78389 2.25586 6.71856 2.625 6.71203ZM2.625 10.6315C2.83008 10.6315 3.02148 10.5955 3.19922 10.5237C3.37695 10.4518 3.53076 10.3506 3.66064 10.2199C3.79053 10.0893 3.89648 9.93903 3.97852 9.76919C4.06055 9.59935 4.10156 9.41644 4.10156 9.22047C4.10156 9.0245 4.06396 8.84159 3.98877 8.67175C3.91357 8.5019 3.80762 8.35493 3.6709 8.23081C3.53418 8.1067 3.37695 8.00544 3.19922 7.92705C3.02148 7.84867 2.83008 7.80947 2.625 7.80947C2.41992 7.80947 2.22852 7.8454 2.05078 7.91726C1.87305 7.98911 1.71924 8.09036 1.58936 8.22101C1.45947 8.35166 1.35352 8.5019 1.27148 8.67175C1.18945 8.84159 1.14844 9.0245 1.14844 9.22047C1.14844 9.41644 1.18604 9.59935 1.26123 9.76919C1.33643 9.93903 1.44238 10.086 1.5791 10.2101C1.71582 10.3342 1.87305 10.4355 2.05078 10.5139C2.22852 10.5923 2.41992 10.6315 2.625 10.6315ZM2.625 12.9831C2.9873 12.9831 3.32568 13.0484 3.64014 13.1791C3.95459 13.3097 4.23486 13.4894 4.48096 13.718C4.72705 13.9467 4.91504 14.2112 5.04492 14.5117C5.1748 14.8122 5.24316 15.1388 5.25 15.4916C5.25 15.8378 5.18164 16.1611 5.04492 16.4616C4.9082 16.7621 4.72021 17.0299 4.48096 17.2651C4.2417 17.5003 3.96484 17.6799 3.65039 17.804C3.33594 17.9281 2.99414 17.9935 2.625 18C2.2627 18 1.92432 17.9347 1.60986 17.804C1.29541 17.6734 1.01514 17.4937 0.769043 17.2651C0.522949 17.0365 0.334961 16.7719 0.205078 16.4714C0.0751953 16.1709 0.00683594 15.8443 0 15.4916C0 15.1453 0.0683594 14.822 0.205078 14.5215C0.341797 14.221 0.529785 13.9532 0.769043 13.718C1.0083 13.4829 1.28516 13.3032 1.59961 13.1791C1.91406 13.055 2.25586 12.9897 2.625 12.9831ZM2.625 16.9026C2.83008 16.9026 3.02148 16.8666 3.19922 16.7948C3.37695 16.7229 3.53076 16.6217 3.66064 16.491C3.79053 16.3604 3.89648 16.2101 3.97852 16.0403C4.06055 15.8704 4.10156 15.6875 4.10156 15.4916C4.10156 15.2956 4.06396 15.1127 3.98877 14.9428C3.91357 14.773 3.80762 14.626 3.6709 14.5019C3.53418 14.3778 3.37695 14.2765 3.19922 14.1981C3.02148 14.1198 2.83008 14.0806 2.625 14.0806C2.41992 14.0806 2.22852 14.1165 2.05078 14.1883C1.87305 14.2602 1.71924 14.3615 1.58936 14.4921C1.45947 14.6228 1.35352 14.773 1.27148 14.9428C1.18945 15.1127 1.14844 15.2956 1.14844 15.4916C1.14844 15.6875 1.18604 15.8704 1.26123 16.0403C1.33643 16.2101 1.44238 16.3571 1.5791 16.4812C1.71582 16.6053 1.87305 16.7066 2.05078 16.785C2.22852 16.8634 2.41992 16.9026 2.625 16.9026ZM1.96875 2.69461L4.78857 0L5.71143 0.881873L1.96875 4.45836L0.194824 2.7632L1.11768 1.88133L1.96875 2.69461Z" fill={isFocused ? "#1E1D1D" : "#C4C8C8"} />
            </Svg>

        </>

    );
};

export default HomeSvg;
